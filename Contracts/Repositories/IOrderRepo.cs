using TiktokLocalAPI.Core.Models.Order;

namespace TiktokLocalAPI.Contracts.Repositories
{
    public interface IOrderRepo
    {
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task<OrderModel?> GetByIdAsync(Guid id);
        Task AddAsync(OrderModel order);
        Task UpdateAsync(OrderModel order);
        Task DeleteAsync(Guid id);
    }
}
