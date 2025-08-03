using Microsoft.Extensions.DependencyInjection;

namespace Framework.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyCorsPolicy",
                    builder =>
                    {
                        builder
                            .WithOrigins(
                                "https://localhost:7255",
                                "http://localhost:8080",
                                "http://192.168.1.3:8080",
                                "http://localhost:3000",
                                "http://192.168.1.3:3000",
                                "http://localhost:30005",
                                "http://192.168.0.220:30005",
                                "https://frontend.unfoldingcloud.com"
                            )
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            });

            return services;
        }
    }
}
