using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.DTO.Chat;
using TiktokLocalAPI.Data.Database;
using TiktokLocalAPI.Data.Models.Chat;

namespace TiktokLocalAPI.Services.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepo _repository;
        private readonly TiktokLocalDbContext _context;

        public ChatService(IChatRepo repository, TiktokLocalDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<ConversationDto>> GetConversationsAsync(Guid userId)
        {
            return await _repository.GetUserConversationsAsync(userId);
        }

        public async Task<ChatRoom> CreateOrGetRoomAsync(Guid user1Id, Guid user2Id)
        {
            return await _repository.GetOrCreateRoomAsync(user1Id, user2Id);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid roomId)
        {
            return await _repository.GetMessagesAsync(roomId);
        }

        public async Task<ChatMessageDto> SendMessageAsync(
            Guid roomId,
            Guid senderId,
            string message
        )
        {
            var sender =
                await _context.Users.FindAsync(senderId) ?? throw new Exception("Sender not found");

            var msg = new ChatMessage
            {
                ChatRoomId = roomId,
                SenderId = senderId,
                Message = message,
                Sender = sender,
                SentAt = DateTime.UtcNow,
            };

            await _repository.AddMessageAsync(msg);
            await _repository.SaveChangesAsync();

            return new ChatMessageDto
            {
                Id = msg.Id,
                Message = msg.Message,
                SentAt = msg.SentAt,
                SenderId = msg.SenderId,
                SenderName = $"{sender.FirstName} {sender.LastName}",
            };
        }

        public async Task MarkMessagesAsReadAsync(Guid roomId, Guid userId)
        {
            var messages = await _context
                .ChatMessages.Where(m =>
                    m.ChatRoomId == roomId && m.SenderId != userId && !m.IsRead
                )
                .ToListAsync();

            foreach (var msg in messages)
            {
                msg.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
