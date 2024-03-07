using fluttyBackend.Service.HardCodeStrings;
using fluttyBackend.Service.services.AuthService.roleVerifier;
using fluttyBackend.Service.services.AuthService.signIn;
using fluttyBackend.Service.services.CartItemService;
using fluttyBackend.Service.services.DeliverySystemService;
using fluttyBackend.Service.services.JwtService;
using fluttyBackend.Service.services.PaymentService;
using fluttyBackend.Service.services.PhotoService;
using fluttyBackend.Service.services.ProductService;
using fluttyBackend.Service.services.RedisService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fluttyBackend.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMyService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAsyncJwtService, AsyncJwtService>(provider =>
            {
                string secretKey = configuration.GetSection("jwtStrings:secret").Value;
                string issuer = configuration.GetSection("jwtStrings:issuer").Value;
                string audience = configuration.GetSection("jwtStrings:audience").Value;
                TimeSpan expiration = TimeSpan.FromHours(5);

                return new AsyncJwtService(secretKey, issuer, audience, expiration);
            });
            services.AddScoped<IAsyncJwtUtil, AsyncJwtUtil>();
            services.AddScoped<IAsyncSignInService, AsyncSignInService>();
            services.AddScoped<IAsyncPhotoService, AsyncPhotoService>();
            services.AddScoped<IAsyncProductService, AsyncProductService>();
            services.AddScoped<IAsyncRoleVerifierService, AsyncRoleVerifierService>();
            // delivery cartitem payment
            services.AddScoped<IAsyncCartItemService, AsyncCartItemService>();
            services.AddScoped<IAsyncPaymentService, AsyncPaymentService>();
            services.AddScoped<IAsyncDeliverySystemService, AsyncDeliverySystemService>();
            // delivery cartitem payment
            services.AddSingleton<AsyncRedisService>();
            // notification
            services.AddSignalR();
            return services;
        }
    }
}
