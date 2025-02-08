using Microsoft.AspNetCore.SignalR;

namespace AdoptivePaws.Application.Services.Notification
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
