using TiktokLocalAPI.Core.DTO.Chat;
using TiktokLocalAPI.Data.Models.Chat;

namespace TiktokLocalAPI.Contracts.Services
{
    public interface IChatService
    {
        Task<List<ConversationDto>> GetConversationsAsync(Guid userId);
        Task<ChatRoom> CreateOrGetRoomAsync(Guid user1Id, Guid user2Id);
        Task<List<ChatMessage>> GetMessagesAsync(Guid roomId);
        Task<ChatMessageDto> SendMessageAsync(Guid roomId, Guid senderId, string message);
        Task MarkMessagesAsReadAsync(Guid roomId, Guid userId);
    }
}
