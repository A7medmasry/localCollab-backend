using Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.DTO.Chat;

namespace TiktokLocalAPI.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service;
        }

        [HttpGet("conversations/{userId}")]
        public async Task<IActionResult> GetUserConversations(Guid userId)
        {
            var conversations = await _service.GetConversationsAsync(userId);
            return Ok(conversations);
        }

        [HttpGet("room/{user1Id}/{user2Id}")]
        public async Task<IActionResult> GetOrCreateRoom(Guid user1Id, Guid user2Id)
        {
            var room = await _service.CreateOrGetRoomAsync(user1Id, user2Id);
            return Ok(room);
        }

        [HttpGet("messages/{roomId}")]
        public async Task<IActionResult> GetMessages(Guid roomId)
        {
            var messages = await _service.GetMessagesAsync(roomId);
            return Ok(messages);
        }

        [HttpPost("read/{roomId}")]
        public async Task<IActionResult> MarkMessagesAsRead(Guid roomId)
        {
            var session = User.GetUserSession();

            await _service.MarkMessagesAsReadAsync(roomId, session.Guid);

            return Ok(new { success = true });
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessageDto request)
        {
            Console.WriteLine($"woooo {request.Message}");

            var session = User.GetUserSession();

            var msg = await _service.SendMessageAsync(
                request.RoomId,
                session.Guid,
                request.Message
            );

            return Ok(msg);
        }
    }
}
