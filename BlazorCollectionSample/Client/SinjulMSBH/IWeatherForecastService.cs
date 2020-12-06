using System.Threading;
using System.Threading.Tasks;

using BlazorCollectionSample.Shared;

namespace BlazorCollectionSample.Client.SinjulMSBH
{
    public interface IWeatherForecastService
    {
        ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default);
    }

    public interface IWeatherForecastService2  
    {
        ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default);
    }

    public interface IWeatherForecastService3
    {
        ValueTask<WeatherForecast[]> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
