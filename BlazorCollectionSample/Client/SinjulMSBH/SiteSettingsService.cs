using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using BlazorCollectionSample.Shared.SinjulMSBH;

namespace BlazorCollectionSample.Client.SinjulMSBH
{
    public class SiteSettingsService : ISiteSettingsService
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public SiteSettingsService(IHttpClientFactory httpClientFactory) => HttpClientFactory = httpClientFactory;


        public async ValueTask<SiteSettings> GetSiteSettingsAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = HttpClientFactory.CreateClient(ConstValue.SiteSettings);

            var result =
                await httpClient.GetFromJsonAsync<SiteSettings>("SiteSettings", cancellationToken);

            return result;
        }
    }
}
