using TiktokLocalAPI.Core.DTO.Chat;
using TiktokLocalAPI.Data.Models.Chat;

namespace TiktokLocalAPI.Contracts.Repositories
{
    public interface IChatRepo
    {
        Task<List<ConversationDto>> GetUserConversationsAsync(Guid userId);
        Task<ChatRoom> GetOrCreateRoomAsync(Guid user1Id, Guid user2Id);
        Task<List<ChatMessage>> GetMessagesAsync(Guid roomId);
        Task AddMessageAsync(ChatMessage message);
        Task SaveChangesAsync();
    }
}
