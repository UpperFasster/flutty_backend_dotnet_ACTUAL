using fluttyBackend.Service.services.DeliverySystemService.DTO.response;

namespace fluttyBackend.Service.services.DeliverySystemService
{
    public interface IAsyncDeliverySystemService
    {
        Task<IEnumerable<InfomationOfOrderDTO>> GetActualOrdersAsync(Guid userId, int page, int size);
        Task<IEnumerable<InfomationOfOrderDTO>> GetCompletedOrdersAsync(Guid userId, int page, int size);
        Task<OrderDetailsDTO> GetOrderDetailsAsync(Guid userId, Guid orderId, int page, int size);
        Task<bool> MarkOrderAsDeliveredAsync(Guid userId, Guid orderId);
        Task<bool> ChangeStatusOrderByAdminAsync(int statusId, Guid orderId, string cause);
        Task<bool> MarkOrderAsCompleted(Guid adminId, Guid orderId);
    }
}