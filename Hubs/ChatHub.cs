// using Microsoft.AspNetCore.SignalR;

// namespace TiktokLocalAPI.Hubs
// {
//     public class ChatHub : Hub
//     {
//         public async Task SendMessageToRoom(string roomId, string userId, string message, string id)
//         {
//             await Clients.Group(roomId).SendAsync("ReceiveMessage", roomId, userId, message, id);
//         }

//         public override async Task OnConnectedAsync()
//         {
//             var httpContext = Context.GetHttpContext();
//             var roomId = httpContext?.Request.Query["roomId"];
//             if (!string.IsNullOrEmpty(roomId))
//             {
//                 await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
//             }
//             await base.OnConnectedAsync();
//         }
//     }
// }

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TiktokLocalAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToRoom(string roomId, string userId, string message, string id)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", roomId, userId, message, id);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task UserTyping(string roomId, string senderId)
        {
            await Clients.OthersInGroup(roomId).SendAsync("UserTyping", roomId, senderId);
        }
    }
}
