using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Data.Models.Chat
{
    public class ChatMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ChatRoomId { get; set; }

        public Guid SenderId { get; set; }
        public required UserModel Sender { get; set; }

        public required string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}
