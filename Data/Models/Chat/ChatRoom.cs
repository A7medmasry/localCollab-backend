using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Data.Models.Chat
{
    public class ChatRoom
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid User1Id { get; set; }
        public required UserModel User1 { get; set; }

        public Guid User2Id { get; set; }
        public required UserModel User2 { get; set; }

        public List<ChatMessage> Messages { get; set; } = new();
    }
}
