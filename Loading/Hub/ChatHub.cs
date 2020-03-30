using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string method, string user, string message)
        {
            await Clients.All.SendAsync(method, user, message);
        }
        
    }
}