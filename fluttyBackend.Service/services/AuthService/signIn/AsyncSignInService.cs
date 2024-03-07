using Dapper;
using fluttyBackend.Domain.Models.UserRoleEntities;
using fluttyBackend.Domain.Models.Utils;
using fluttyBackend.Service.HardCodeStrings;
using fluttyBackend.Service.services.AuthService.signIn.DTO.request;
using fluttyBackend.Service.services.AuthService.signIn.DTO.response;
using fluttyBackend.Service.services.JwtService;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace fluttyBackend.Service.services.AuthService.signIn
{
    public class AsyncSignInService : IAsyncSignInService
    {
        private readonly IAsyncJwtService jwtAsyncService;
        private readonly string connectionString;

        public AsyncSignInService(
            IAsyncJwtService jwtAsyncService,
            IConfiguration configuration)
        {
            this.jwtAsyncService = jwtAsyncService;
            connectionString = configuration.GetConnectionString(
                ConnectionStringNames.DataBaseConnectionString
            );
        }

        public async Task<SuccessfullySignInDTOResponse> SignIn(UserSignInDTO userSignIn)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = $@"
                    SELECT *
                    FROM {EntityNamesConstants.User}
                    WHERE ""{nameof(User.Email)}"" = @Email
                    AND ""{nameof(User.Password)}"" = @Password";

                var user = connection.QueryFirstOrDefault<User>(sql, new { userSignIn.Email, userSignIn.Password });

                if (user != null)
                {
                    var token = await jwtAsyncService.GenerateTokenAsync(user.UserId.ToString());
                    var signInDTO = new SuccessfullySignInDTOResponse
                    {
                        Token = token,
                    };

                    return signInDTO;
                }
                // todo make normal exception
                throw new FileNotFoundException();
            }
        }
    }
}
