using Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.DTO.Order;
using TiktokLocalAPI.Core.DTO.Subscription;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Data.Database;
using static TiktokLocalAPI.Core.Helpers.ProgramExtensions;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly TiktokLocalDbContext _context;

    public ReviewsController(TiktokLocalDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDto dto)
    {
        var existingReview = await _context.Reviews.FirstOrDefaultAsync(r =>
            r.OrderId == dto.OrderId
        );
        if (existingReview != null)
            return Conflict("A review for this order already exists.");
        var fromUser = await _context.Users.FindAsync(dto.FromUserId);
        var toUser = await _context.Users.FindAsync(dto.ToUserId);
        var order = await _context.Orders.FindAsync(dto.OrderId);

        if (fromUser == null || toUser == null || order == null)
            return BadRequest("Invalid user or order reference.");

        var review = new ReviewModel
        {
            OrderId = dto.OrderId,
            Order = order,
            FromUserId = dto.FromUserId,
            FromUser = fromUser,
            ToUserId = dto.ToUserId,
            ToUser = toUser,
            CommunicationRating = dto.CommunicationRating,
            DeliveryRating = dto.DeliveryRating,
            QualityRating = dto.QualityRating,
            Comment = dto.Comment,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var response = new ReviewResponseDto
        {
            Id = review.Id,
            OrderId = review.OrderId,
            FromUserId = review.FromUserId,
            ToUserId = review.ToUserId,
            CommunicationRating = review.CommunicationRating,
            DeliveryRating = review.DeliveryRating,
            QualityRating = review.QualityRating,
            Comment = review.Comment,
        };

        return CreatedAtAction(nameof(GetReview), new { id = review.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewEditDto dto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound("Review not found.");
        }

        // Update the review fields
        review.ProviderRating = dto.ProviderRating;
        review.ProviderComment = dto.ProviderComment;

        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();

        return Ok(review);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReview(Guid id)
    {
        var review = await _context
            .Reviews.Include(r => r.FromUser)
            .Include(r => r.ToUser)
            .Include(r => r.Order)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (review == null)
            return NotFound();

        return Ok(review);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsForUser(Guid userId)
    {
        var reviews = await _context.Reviews.Where(r => r.ToUserId == userId).ToListAsync();

        return Ok(reviews);
    }
}
