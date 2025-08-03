using Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.DTO.Subscription;
using TiktokLocalAPI.Core.DTO.User;
using static TiktokLocalAPI.Core.Helpers.ProgramExtensions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly CustomerService _customerService;
    private readonly SubscriptionService _subscriptionService;
    private readonly PaymentMethodService _paymentMethodService;
    private readonly StripeSettings _stripeSettings;

    private readonly IUserService _userService;

    public SubscriptionController(
        CustomerService customerService,
        SubscriptionService subscriptionService,
        PaymentMethodService paymentMethodService,
        IOptions<StripeSettings> stripeSettings,
        IUserService userService
    )
    {
        _customerService = customerService;
        _subscriptionService = subscriptionService;
        _paymentMethodService = paymentMethodService;
        _stripeSettings = stripeSettings.Value;

        _userService = userService;

        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionDto request)
    {
        var session = User.GetUserSession();

        try
        {
            // 1. Create or retrieve the customer
            var customerOptions = new CustomerCreateOptions
            {
                Email = request.Email,
                PaymentMethod = request.PaymentMethodId,
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = request.PaymentMethodId,
                },
            };

            var customer = await _customerService.CreateAsync(customerOptions);

            // 2. Attach payment method to customer
            await _paymentMethodService.AttachAsync(
                request.PaymentMethodId,
                new PaymentMethodAttachOptions { Customer = customer.Id }
            );

            // 3. Create the subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customer.Id,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions { Price = request.PriceId },
                },
                Expand = new List<string> { "latest_invoice.payment_intent" },
            };

            var subscription = await _subscriptionService.CreateAsync(subscriptionOptions);
            // 4. Update user profile

            // Map of PriceId => (Credits, PlanName)
            var pricePlans = new Dictionary<string, (int Credits, string Plan)>
            {
                { "price_1RrY8GKAWz1qORTn78BUFB0k", (100, "Starter") },
                { "price_1Rrb3CKAWz1qORTn32svlimD", (250, "Creator") },
                { "price_1Rrb5jKAWz1qORTn8v35LRF9", (500, "Pro") },
                { "price_1Rrb7NKAWz1qORTnlbMvlkSQ", (1000, "Enterprise") },
            };

            // Try to get plan info
            var (credits, plan) = pricePlans.TryGetValue(request.PriceId, out var planInfo)
                ? planInfo
                : (0, "Unknown");

            // Create the DTO
            var userUpdateDto = new UserEditDto
            {
                StripeCustomerId = customer.Id,
                SubscriptionId = subscription.Id,
                IsActive = true,
                ValidCredits = DateTime.UtcNow.AddDays(28),
                Credits = credits,
                Plan = plan,
            };

            var User = await _userService.UpdateCreditsProfile(session.Guid, userUpdateDto);
            return Ok(
                new
                {
                    SubscriptionId = subscription.Id,
                    subscription = subscription.LatestInvoice,
                    Status = subscription.Status,
                    User = User,
                }
            );
        }
        catch (StripeException e)
        {
            return BadRequest(new { Error = new { Message = e.StripeError.Message } });
        }
    }
}
