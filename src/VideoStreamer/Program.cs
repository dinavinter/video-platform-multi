using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VideoStreamer.Auth;
using VideoStreamer.Shared;

namespace VideoStreamer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
      
              builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
  
            //   builder.Services.AddHttpClient("Streamer.Identity.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //     ;//     .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            // builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Streamer.Identity.ServerAPI"));
            builder.Services.AddAuthorizationCore();


              builder.Services.ConfigureServices(builder.Configuration);


              await builder.Build().RunAsync();

            // builder.Services.AddOidcAuthentication(options =>
            // {
            //     // Configure your authentication provider options here.
            //     // For more information, see https://aka.ms/blazor-standalone-auth
            //     builder.Configuration.Bind("Local", options.ProviderOptions);
            // });

            //storage
             

 
            // builder.Services.AddHttpClient("Streamer.Identity.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //     .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            //
            // // Supply HttpClient instances that include access tokens when making requests to the server project
            // builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Streamer.Identity.ServerAPI"));
            // builder.Services.AddApiAuthorization();

 
        }
    }
    
    
}