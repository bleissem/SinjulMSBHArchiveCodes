
using System.Threading;
using System.Threading.Tasks;

using BlazorCollectionSample.Shared.SinjulMSBH;

namespace BlazorCollectionSample.Client.SinjulMSBH
{
    public interface ISiteSettingsService
    {
        ValueTask<SiteSettings> GetSiteSettingsAsync(CancellationToken cancellationToken = default);
    }
}
