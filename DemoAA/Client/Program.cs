using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DemoAA.Client.SinjulMSBH.StateProvider;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DemoAA.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("DemoAA.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DemoAA.ServerAPI"));

            ConfigureCommonServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureCommonServices(IServiceCollection services)
        {
            // services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            services.AddOptions();
            services.AddApiAuthorization();
            //services.AddAuthorizationCore();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("content-editor", policy =>
                    policy
                        .RequireAuthenticatedUser()
                        .RequireRole(new string[] { "admin" })
                );
            });
        }
    }
}
