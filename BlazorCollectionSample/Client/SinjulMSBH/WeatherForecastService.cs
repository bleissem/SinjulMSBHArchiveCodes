using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using BlazorCollectionSample.Shared;

namespace BlazorCollectionSample.Client.SinjulMSBH
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public HttpClient HttpClient { get; }

        public WeatherForecastService(HttpClient httpClient) => HttpClient = httpClient;


        public async ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result =
                await HttpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast", cancellationToken);

            return result;
        }
    }

    public class WeatherForecastService2 : IWeatherForecastService2
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public WeatherForecastService2(IHttpClientFactory httpClientFactory) => HttpClientFactory = httpClientFactory;


        public async ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = HttpClientFactory.CreateClient(ConstValue.WeatherForecast2);

            var result =
                await httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast", cancellationToken);

            return result;
        }
    }

    public class WeatherForecastService3 : IWeatherForecastService3
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public WeatherForecastService3(IHttpClientFactory httpClientFactory) => HttpClientFactory = httpClientFactory;


        public async ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = HttpClientFactory.CreateClient(ConstValue.WeatherForecast3);

            var result =
                await httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast", cancellationToken);

            return result;
        }
    }
}
