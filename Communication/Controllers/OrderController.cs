using Framework.Extensions;
using Framework.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.Order;
using TiktokLocalAPI.Core.Models.Order;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) => _orderService = orderService;

    [HttpGet("to")]
    public async Task<IActionResult> GetAllToUser()
    {
        var session = User.GetUserSession();

        var orders = await _orderService.GetAllOrderToUser(session.Guid);

        return QlResult.Success(InformationMessages.UserRetrieved, orders);
    }

    [HttpGet("from")]
    public async Task<IActionResult> GetAllFromUser()
    {
        var session = User.GetUserSession();

        var orders = await _orderService.GetAllOrderFromUser(session.Guid);

        return QlResult.Success(InformationMessages.UserRetrieved, orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderCreateDto orderDto)
    {
        Console.WriteLine($"wooo {User.GetUserSession().Guid} ||| {orderDto.ServiceId}");

        var session = User.GetUserSession();

        var order = await _orderService.AddOrder(session.Guid, orderDto);

        return QlResult.Success(InformationMessages.UserRetrieved, order);
    }

    [HttpPost("{orderId}/{status}")]
    public async Task<IActionResult> AcceptOrder(Guid orderId, string status)
    {
        var session = User.GetUserSession();
        await _orderService.StatusOrderAsync(session.Guid, orderId, status);
        return Ok(new { message = $"Order {status} successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderModel updated)
    {
        if (id != updated.Id)
            return BadRequest();
        await _orderService.UpdateOrder(updated);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _orderService.DeleteOrder(id);
        return NoContent();
    }
}
