using TiktokLocalAPI.Core.DTO.Order;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.OutDto.Order;

namespace TiktokLocalAPI.Contracts.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderOutDto>> GetAllOrderToUser(Guid requestorGuid);
        Task<IEnumerable<OrderOutDto>> GetAllOrderFromUser(Guid requestorGuid);
        Task<OrderModel?> GetOrderById(Guid id);
        Task<OrderOutDto> AddOrder(Guid requestorGuid, OrderCreateDto order);
        Task StatusOrderAsync(Guid requestorGuid, Guid orderId, string status);
        Task UpdateOrder(OrderModel order);
        Task DeleteOrder(Guid id);
    }
}
