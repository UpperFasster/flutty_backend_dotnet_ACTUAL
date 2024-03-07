using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace fluttyBackend.Service.services.NotificationService
{
    public class SignalRService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IDistributedCache _cache;

        public SignalRService(IHubContext<ChatHub> hubContext, IDistributedCache cache)
        {
            _hubContext = hubContext;
            _cache = cache;
        }

        public async Task SendNotificationToUser(string userId, string message)
        {
            // Retrieve user list from Redis
            var userList = await _cache.GetStringAsync("userList");

            // Perform operations with userList as needed

            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}