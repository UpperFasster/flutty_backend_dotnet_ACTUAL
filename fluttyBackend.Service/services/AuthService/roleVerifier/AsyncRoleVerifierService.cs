
using System.Data;
using System.Net;
using Dapper;
using fluttyBackend.Domain.Models.Company;
using fluttyBackend.Domain.Models.Utils;
using fluttyBackend.Service.exceptions;
using fluttyBackend.Service.HardCodeStrings;
using fluttyBackend.Service.services.RedisService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace fluttyBackend.Service.services.AuthService.roleVerifier
{

    public class CompanyDTO
    {
        public Guid CompanyId { get; set; }
        public HashSet<Guid> Employees { get; set; }
        public bool IsBlocked { get; set; }
        public bool Approved { get; set; }
    }

    public class AsyncRoleVerifierService : IAsyncRoleVerifierService
    {

        private readonly AsyncRedisService redisService;
        private readonly IDbConnection dbConnection;
        public AsyncRoleVerifierService(
            AsyncRedisService redisService,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(
                ConnectionStringNames.DataBaseConnectionString
            );
            // todo fix code here
            dbConnection = new NpgsqlConnection(connectionString);
            this.redisService = redisService;
        }

        public async Task<bool> IsUserFounderOrEmployeeAsync(Guid userId, Guid companyId)
        {
            var redisQueryResult = await redisService.GetAsync<CompanyDTO>(companyId.ToString());

            if (redisQueryResult == null)
            {
                string query = @$"
                    SELECT c.""{nameof(CompanyTbl.Id)}"" as CompanyId, 
                    e.""{nameof(OtMCompanyEmployees.EmployeeId)}"", 
                    c.""{nameof(CompanyTbl.Blocked)}"", 
                    c.""{nameof(CompanyTbl.Approved)}""
                    FROM {EntityNamesConstants.Company} AS c
                    LEFT JOIN {EntityNamesConstants.OtMCompanyEmployees} AS e ON 
                    c.""{nameof(CompanyTbl.Id)}"" = e.""{nameof(OtMCompanyEmployees.CompanyId)}""
                    WHERE (c.""{nameof(CompanyTbl.FounderId)}"" = @UserId AND 
                    c.""{nameof(CompanyTbl.Id)}"" = @CompanyId) 
                    OR (e.""{nameof(OtMCompanyEmployees.EmployeeId)}"" = @UserId AND 
                    e.""{nameof(OtMCompanyEmployees.CompanyId)}"" = @CompanyId)";

                var result = dbConnection.Query<CompanyDTO, Guid, CompanyDTO>(
                    query,
                    (company, employeeId) =>
                    {
                        company.Employees ??= new HashSet<Guid>();
                        company.Employees.Add(employeeId);
                        return company;
                    },
                    new { UserId = userId, CompanyId = companyId },
                    splitOn: $"{nameof(OtMCompanyEmployees.EmployeeId)}"
                ).Distinct().FirstOrDefault() ?? throw new AccessDeniedException();

                if (await redisService.SetAsync<CompanyDTO>(result.CompanyId.ToString(), result, TimeSpan.FromHours(1)))
                {
                    // todo make other exception
                    throw new Exception("No connection to Redis");
                }

                redisQueryResult = result;
            }

            // check is that company not blocked
            if (redisQueryResult.IsBlocked)
            {
                throw new AccessDeniedException();
            }

            if (redisQueryResult.Employees.Contains(userId))
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
