
using BlazorCollectionSample.Shared.SinjulMSBH;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlazorCollectionSample.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteSettingsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<SiteSettings> Get([FromServices] IOptionsSnapshot<SiteSettings> siteSettings) => siteSettings.Value;
    }
}
