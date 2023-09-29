using Microsoft.AspNetCore.SignalR;

namespace ASPCORE.Models
{
    public class NotificationHub:Hub
    {
        //public async Task SendMessage(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMsg", message);
        //}

        public override Task OnConnectedAsync()
        {
            ConnectedUsers.UserId?.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers.UserId?.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
