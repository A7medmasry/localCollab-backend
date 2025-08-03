namespace TiktokLocalAPI.Core.DTO.Chat
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; } // user2 or user1 depending on current user
        public required string Avatar { get; set; } // initials
        public required string LastMessage { get; set; }
        public DateTime? Time { get; set; } // formatted time e.g. "2h ago"
        public int Unread { get; set; }
    }
}
