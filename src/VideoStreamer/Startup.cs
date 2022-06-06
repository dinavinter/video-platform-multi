using System;
using System.Net.Http;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VideoStreamer.Auth;
using VideoStreamer.Shared;

namespace VideoStreamer
{
    public static class StartupEx
    {
  
        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddLogging(builder => { });
            services.AddBlazoredLocalStorage();

            services.Configure<TokenSettings>(settings =>
            {
                settings.Key = configuration.GetSection("TokenSettings").GetValue<string>("Key");
                settings.Issuer=  configuration.GetSection("TokenSettings").GetValue<string>("Issuer");
                
            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<TokenAuthenticationProvider>();
            services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(sp=>sp.GetService<TokenAuthenticationProvider>());
            // services.AddSingleton<IAuthorizationHandler, HasResourceHandler>();
            
            // services.Configure<AuthorizationOptions>(options =>
            // {
            //     options.AddPolicy("read:messages",
            //         policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            //     options.AddPolicy("create:messages",
            //         policy => policy.Requirements.Add(new HasScopeRequirement("create:messages", domain)));
            //
            // });
        }
        
        
        
    }

     
}