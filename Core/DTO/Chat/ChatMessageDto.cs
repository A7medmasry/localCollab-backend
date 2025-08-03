namespace TiktokLocalAPI.Core.DTO.Chat
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Guid SenderId { get; set; }
        public required string SenderName { get; set; }
    }
}
