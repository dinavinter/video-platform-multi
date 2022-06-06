using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using AuthLib.Integration;
using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Streamer.Identity.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
            );
            
              //AddIdentityClient(builder);
             AddLocalClient(builder);
            await builder.Build().RunAsync();
        }

        private static void AddLocalClient(WebAssemblyHostBuilder builder)
        { 
            builder.Services.AddHttpClient("Streamer.Identity.ClientApi",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
 
            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Streamer.Identity.ClientApi"));
            
            builder.Services.AddLocalAuthontication(builder.Configuration);
            

        }

        private static void AddIdentityClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddLocalAuthontication(builder.Configuration);

            builder.Services.AddHttpClient("Streamer.Identity.ServerAPI",
                    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Streamer.Identity.ServerAPI"));
            
            builder.Services.AddApiAuthorization();

        }
    }
}
