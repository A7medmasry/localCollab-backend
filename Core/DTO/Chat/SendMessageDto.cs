namespace TiktokLocalAPI.Core.DTO.Chat
{
    public class SendMessageDto
    {
        public Guid RoomId { get; set; }
        public required string Message { get; set; }
    }
}
