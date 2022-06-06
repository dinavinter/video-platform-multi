using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VideoStreamer.Auth;
using VideoStreamer.Shared;
using Microsoft.Extensions.Configuration;
namespace AuthLib.Integration
{
    public static class ex
    {
        public static void AddLocalAuthontication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.Configure<TokenSettings>(settings =>
            {
                settings.Key = configuration.GetSection("TokenSettings").GetValue<string>("Key");
                settings.Issuer = configuration.GetSection("TokenSettings").GetValue<string>("Issuer");

            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<TokenAuthenticationProvider>();
            services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(sp =>
                sp.GetService<TokenAuthenticationProvider>());

            services.AddScoped<IAuthorizationHandler, HasResourceHandler>();

        }
    }
}