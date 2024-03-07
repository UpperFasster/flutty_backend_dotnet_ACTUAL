using Dapper;
using fluttyBackend.Domain.Models.DeliveryProduct;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.Utils;
using fluttyBackend.Service.HardCodeStrings;
using fluttyBackend.Service.services.DeliverySystemService.DTO.response;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace fluttyBackend.Service.services.DeliverySystemService
{
    public class AsyncDeliverySystemService : IAsyncDeliverySystemService
    {
        private readonly string connectionString;

        public AsyncDeliverySystemService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString(
                ConnectionStringNames.DataBaseConnectionString
            );
        }

        public async Task<IEnumerable<InfomationOfOrderDTO>> GetActualOrdersAsync(Guid userId, int page, int size)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                    SELECT
                        d.""{nameof(DeliveryOrderProduct.Id)}"",
                        d.""{nameof(DeliveryOrderProduct.OrderId)}"",
                        d.""{nameof(DeliveryOrderProduct.UtcWillFinishedTime)}"" AS {nameof(InfomationOfOrderDTO.WillBeReadyUtcTime)},
                        o.""{nameof(OtMOrderProduct.ByPrice)}"",
                        d.""{nameof(DeliveryOrderProduct.Address)}"",
                        d.""{nameof(DeliveryOrderProduct.Coordinate)}"",
                        d.""{nameof(DeliveryOrderProduct.Status)}"",
                        p.""{nameof(Product.Photo)}""
                    FROM
                        {EntityNamesConstants.DeliveryProduct} d
                    JOIN
                        {EntityNamesConstants.OtMDeliveryProduct} o 
                            ON d.""{nameof(DeliveryOrderProduct.Id)}"" = o.""{nameof(OtMOrderProduct.DeliveryOrderId)}""
                    JOIN
                        {EntityNamesConstants.Product} p ON o.""{nameof(OtMOrderProduct.ProductId)}"" = p.""{nameof(Product.Id)}""
                    WHERE
                        d.{nameof(DeliveryOrderProduct.UserId)} = @UserId
                        AND d.""{nameof(DeliveryOrderProduct.Finished)}"" = false
                    ORDER BY d.""{nameof(DeliveryOrderProduct.UtcWillFinishedTime)}""
                    OFFSET @Offset ROWS
                    FETCH NEXT @Size ROWS ONLY";

                var result = await connection.QueryAsync<InfomationOfOrderDTO>(
                    query,
                    new
                    {
                        UserId = userId,
                        Offset = (page - 1) * size,
                        Size = size
                    });

                return result;
            }
        }

        public async Task<IEnumerable<InfomationOfOrderDTO>> GetCompletedOrdersAsync(Guid userId, int page, int size)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                    SELECT
                        d.""{nameof(DeliveryOrderProduct.Id)}"",
                        d.""{nameof(DeliveryOrderProduct.OrderId)}"",
                        d.""{nameof(DeliveryOrderProduct.UtcWillFinishedTime)}"" AS {nameof(InfomationOfOrderDTO.WillBeReadyUtcTime)},
                        o.""{nameof(OtMOrderProduct.ByPrice)}"",
                        d.""{nameof(DeliveryOrderProduct.Address)}"",
                        d.""{nameof(DeliveryOrderProduct.Coordinate)}"",
                        d.""{nameof(DeliveryOrderProduct.Status)}"",
                        p.""{nameof(Product.Photo)}""
                    FROM
                        {EntityNamesConstants.DeliveryProduct} d
                    JOIN
                        {EntityNamesConstants.OtMDeliveryProduct} o 
                            ON d.""{nameof(DeliveryOrderProduct.Id)}"" = o.""{nameof(OtMOrderProduct.DeliveryOrderId)}""
                    JOIN
                        {EntityNamesConstants.Product} p ON o.""{nameof(OtMOrderProduct.ProductId)}"" = p.""{nameof(Product.Id)}""
                    WHERE
                        d.{nameof(DeliveryOrderProduct.UserId)} = @UserId
                        AND d.""{nameof(DeliveryOrderProduct.Finished)}"" = true
                    ORDER BY d.""{nameof(DeliveryOrderProduct.UtcWillFinishedTime)}""
                    OFFSET @Offset ROWS
                    FETCH NEXT @Size ROWS ONLY";

                var result = await connection.QueryAsync<InfomationOfOrderDTO>(
                    query,
                    new
                    {
                        UserId = userId,
                        Offset = (page - 1) * size,
                        Size = size
                    });

                return result;
            }
        }

        public async Task<OrderDetailsDTO> GetOrderDetailsAsync(Guid userId, Guid orderId, int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MarkOrderAsCompleted(Guid adminId, Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangeStatusOrderByAdminAsync(int statusId, Guid orderId, string cause)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> MarkOrderAsDeliveredAsync(Guid userId, Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}