using Framework.Exceptions;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.Order;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.OutDto.Order;
using TiktokLocalAPIs.Core.Enums;

namespace TiktokLocalAPI.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repository;
        private readonly IUserRepo _userRepo;
        private readonly IServiceRepo _serviceRepo;

        public OrderService(IOrderRepo repository, IUserRepo userRepo, IServiceRepo serviceRepo)
        {
            _repository = repository;
            _userRepo = userRepo;
            _serviceRepo = serviceRepo;
        }

        public async Task<IEnumerable<OrderOutDto>> GetAllOrderToUser(Guid requestorGuid)
        {
            var orders = await _repository.GetAllAsync();
            return orders
                .Where(order => order.ToUserId == requestorGuid)
                .Select(order => order.ToOutDto());
        }

        public async Task<IEnumerable<OrderOutDto>> GetAllOrderFromUser(Guid requestorGuid)
        {
            var orders = await _repository.GetAllAsync();
            return orders
                .Where(order => order.FromUserId == requestorGuid)
                .Select(order => order.ToOutDto());
        }

        public Task<OrderModel?> GetOrderById(Guid id) => _repository.GetByIdAsync(id);

        public async Task<OrderOutDto> AddOrder(Guid requestorGuid, OrderCreateDto dto)
        {
            var FromUser = await _userRepo.GetUser(requestorGuid);
            if (FromUser == null)
                throw new QlBadRequestException(ExceptionMessages.AccountStatusUpdateFailed);

            var ToUser = await _userRepo.GetUser(dto.ToUserId);
            if (ToUser == null)
                throw new QlBadRequestException(ExceptionMessages.AccountStatusUpdateFailed);

            var order = new OrderModel
            {
                ServiceId = dto.ServiceId,
                Amount = dto.Amount,
                StartDate = dto.StartDate,
                DeliveryDate = dto.DeliveryDate,
                Description = dto.Description,
                ToUserId = ToUser.Id,
                FromUserId = FromUser.Id,
            };

            await _repository.AddAsync(order);
            return order.ToOutDto();
        }

        public async Task StatusOrderAsync(Guid requestorGuid, Guid orderId, string status)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order == null)
                throw new QlNotFoundException("Order not found");

            if (order.ToUserId != requestorGuid)
                throw new QlForbiddenException("You are not authorized to accept this order");

            if (status == "accepted")
            {
                order.Status = OrderStatus.Accepted;
            }
            if (status == "progress")
            {
                order.Status = OrderStatus.Progress;
            }
            if (status == "rejected")
            {
                order.Status = OrderStatus.Rejected;
            }
            if (status == "completed")
            {
                order.Status = OrderStatus.Completed;
            }
            if (status == "cancelled")
            {
                order.Status = OrderStatus.Cancelled;
            }

            await _repository.UpdateAsync(order);
        }

        public Task UpdateOrder(OrderModel order) => _repository.UpdateAsync(order);

        public Task DeleteOrder(Guid id) => _repository.DeleteAsync(id);
    }
}
