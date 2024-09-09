using Microsoft.AspNetCore.SignalR;

namespace PacMen.API.Hubs
{
    public class PacMenHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
