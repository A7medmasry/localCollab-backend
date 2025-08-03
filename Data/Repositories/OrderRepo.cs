using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Data.Database;

namespace TiktokLocalAPI.Data.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly TiktokLocalDbContext _context;

        public OrderRepo(TiktokLocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync() =>
            await _context
                .Orders.Include(o => o.Service)
                .Include(o => o.FromUser)
                .Include(o => o.ToUser)
                .Include(o => o.Review)
                .ToListAsync();

        public async Task<OrderModel?> GetByIdAsync(Guid id) =>
            await _context
                .Orders
                // .Include(o => o.Service)
                .Include(o => o.Review)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(OrderModel order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderModel order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
