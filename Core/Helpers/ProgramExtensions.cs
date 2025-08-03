using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Stripe;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Data.Database;
using TiktokLocalAPI.Data.Repositories;
using TiktokLocalAPI.Helpers;
using TiktokLocalAPI.Services.Services;

namespace TiktokLocalAPI.Core.Helpers
{
    /// <summary>
    /// Contains extension methods for configuring services used by the Identity API.
    /// </summary>
    public static class ProgramExtensions
    {
        /// <summary>
        /// Adds and configures Swagger/OpenAPI with XML documentation.
        /// </summary>
        /// <param name="services">The service collection to extend.</param>
        public static void AddXmlDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Identity API",
                        Version = "v1",
                        Description = "Identity related API",
                    }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Adds and configures MySQL database context using connection string from environment variables.
        /// </summary>
        /// <param name="services">The service collection to extend.</param>
        public static void AddMySQL(this IServiceCollection services)
        {
            string connectionString = MySqlHelper.GetDbConnectionString();

            services.AddDbContext<TiktokLocalDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
        }

        /// <summary>
        /// Registers repository interfaces and their implementations in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to extend.</param>
        public static void AddIdentityRepos(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ISessionRepo, SessionRepo>();
        }

        public static void AddServiceRepos(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepo, ServiceRepo>();
        }

        public static void AddChatRepos(this IServiceCollection services)
        {
            services.AddScoped<IChatRepo, ChatRepo>();
        }

        public static void AddOrderRepos(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepo, OrderRepo>();
        }

        /// <summary>
        /// Registers service interfaces and their implementations in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to extend.</param>
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
        }

        public static void AddChatServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddSignalR();
        }

        public static void AddOrderServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        public static void AddFileServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, TiktokLocalAPI.Services.Services.FileService>();
            // services.AddScoped<IFileService, FileService>();
        }

        public class StripeSettings
        {
            public string? PublishableKey { get; set; }
            public string? SecretKey { get; set; }
            public string? WebhookSecret { get; set; }
        }

        public static void AddStripeInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddScoped<CustomerService>();
            services.AddScoped<PriceService>();
            services.AddScoped<ProductService>();
            services.AddScoped<SubscriptionService>();
            services.AddScoped<PaymentMethodService>();
            services.AddScoped<PaymentIntentService>();

            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
        }

        /// <summary>
        /// Configures JWT authentication and token validation, including token expiration handling.
        /// </summary>
        /// <param name="services">The service collection to extend.</param>
        /// <param name="configuration">The application configuration from which JWT settings are read.</param>
        public static void AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                        ),
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is SecurityTokenExpiredException)
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(
                                    new { message = "token expired" }
                                );
                                return context.Response.WriteAsync(result);
                            }

                            return Task.CompletedTask;
                        },
                    };
                });
        }
    }
}
