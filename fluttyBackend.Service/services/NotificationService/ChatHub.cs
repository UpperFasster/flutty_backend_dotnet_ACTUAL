using Microsoft.AspNetCore.SignalR;

namespace fluttyBackend.Service.services.NotificationService
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReciveMessage", $"{Context.ConnectionId} has joined");
        }

        


    }
}