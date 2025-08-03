using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Core.DTO.Chat;
using TiktokLocalAPI.Data.Database;
using TiktokLocalAPI.Data.Models.Chat;

namespace TiktokLocalAPI.Data.Repositories
{
    public class ChatRepo : IChatRepo
    {
        private readonly TiktokLocalDbContext _context;

        public ChatRepo(TiktokLocalDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConversationDto>> GetUserConversationsAsync(Guid userId)
        {
            var rooms = await _context
                .ChatRooms.Include(r => r.Messages.OrderByDescending(m => m.SentAt))
                .Include(r => r.User1)
                .Include(r => r.User2)
                .Where(r => r.User1Id == userId || r.User2Id == userId)
                .ToListAsync();

            var conversations = rooms
                .Select(r =>
                {
                    var otherUser = r.User1Id == userId ? r.User2 : r.User1;
                    var lastMessage = r.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault();

                    return new ConversationDto
                    {
                        Id = r.Id,
                        Name = $"{otherUser.FirstName} {otherUser.LastName}",
                        Avatar = otherUser?.Avatar ?? "",
                        LastMessage = lastMessage?.Message ?? "",
                        UserId = otherUser.Id,
                        Time =
                            lastMessage != null
                                ? DateTime.SpecifyKind(lastMessage.SentAt, DateTimeKind.Utc)
                                : null,
                        Unread = r.Messages.Count(m => !m.IsRead && m.SenderId != userId),
                    };
                })
                .ToList();

            return conversations;
        }

        public async Task<ChatRoom> GetOrCreateRoomAsync(Guid user1Id, Guid user2Id)
        {
            Console.WriteLine($"wooo3 error from here");

            var room = await _context
                .ChatRooms.Include(r => r.Messages)
                .FirstOrDefaultAsync(r =>
                    (r.User1Id == user1Id && r.User2Id == user2Id)
                    || (r.User1Id == user2Id && r.User2Id == user1Id)
                );
            Console.WriteLine($"wooo2 {room}");

            if (room != null)
                return room;

            var user1 =
                await _context.Users.FindAsync(user1Id) ?? throw new Exception("User 1 Not found");

            var user2 =
                await _context.Users.FindAsync(user2Id) ?? throw new Exception("User 2 Not found");
            Console.WriteLine($"wooo {user1.FirstName} {user2.FirstName}");
            room = new ChatRoom
            {
                User1Id = user1Id,
                User2Id = user2Id,
                User1 = user1,
                User2 = user2,
            };

            _context.ChatRooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid roomId)
        {
            var messages = await _context
                .ChatMessages.Where(m => m.ChatRoomId == roomId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            // Apply UTC Kind after retrieval
            foreach (var msg in messages)
            {
                msg.SentAt = DateTime.SpecifyKind(msg.SentAt, DateTimeKind.Utc);
            }

            return messages;
        }

        public async Task AddMessageAsync(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
