using System;
using System.Net.Http;
using System.Threading.Tasks;

using BlazorCollectionSample.Client.SinjulMSBH;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorCollectionSample.Shared.SinjulMSBH;

namespace BlazorCollectionSample.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("BlazorCollectionSample.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            ;

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorCollectionSample.ServerAPI"));

            builder.Services.AddApiAuthorization();


            //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘


            var baseAddress = builder.HostEnvironment.BaseAddress;

            builder.Services.AddHttpClient<ISiteSettingsService, SiteSettingsService>
                (nameof(ConstValue.SiteSettings),
                    client => client.BaseAddress = new Uri(baseAddress));

            builder.Services.AddHttpClient<IWeatherForecastService2, WeatherForecastService2>
                    (nameof(ConstValue.WeatherForecast2),
                        client => client.BaseAddress = new Uri(baseAddress));

            builder.Services.AddHttpClient<IWeatherForecastService3, WeatherForecastService3>
                    (nameof(ConstValue.WeatherForecast3),
                        client => client.BaseAddress = new Uri(baseAddress));

            builder.Services.AddTransient<ISiteSettingsService, SiteSettingsService>();
            builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddTransient<IWeatherForecastService2, WeatherForecastService2>();
            builder.Services.AddTransient<IWeatherForecastService3, WeatherForecastService3>();


            //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
            //builder.Services.AddOptions<SiteSettings>()
            //      .Bind(builder.Configuration.GetSection(nameof(SiteSettings)))
            //      .ValidateDataAnnotations();

            var http = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            builder.Services.AddScoped(sp => http);
            using var response = await http.GetAsync("cars.json");
            using var stream = await response.Content.ReadAsStreamAsync();
            builder.Configuration.AddJsonStream(stream);


            //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘


            await builder.Build().RunAsync();
        }
    }
}
