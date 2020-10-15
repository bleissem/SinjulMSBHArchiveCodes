using System.Threading;

using Microsoft.AspNetCore.Mvc.Filters;

namespace MySignalR.Hubs
{
    public class LanguageFilterAttribute : ActionFilterAttribute
    {
        public int FilterArgument { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string cultureName = null;
            var cultureCookie = request.Cookies["_culture"];

            //if (request.UserLanguages != null) 
            //    cultureName = cultureCookie != null ? cultureCookie.Value : request.UserLanguages[0];

            //cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
            cultureName = "en-US";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.OnActionExecuting(filterContext);
        }
    }

    //public class LanguageFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        var request = filterContext.HttpContext.Request;
    //        string currentUrl = request.RawUrl;
    //        var urlHelper = new UrlHelper(request.RequestContext);

    //        string baseurl = urlHelper.Content("~");
    //        string currentLanguageFromUrl = currentUrl.Split('/')[1];
    //        string currentLanguageFromCulture = CultureHelper.CheckCulture();
    //        var currentLanguageFromCookie = request.Cookies["_culture"];

    //        var possibleCultures = UnitOfWork.CulturesRepository.GetListOfCultureNames();

    //        if (possibleCultures.All(culture => currentLanguageFromUrl != culture))
    //        {
    //            string cultureName;
    //            string newUrl;

    //            if (currentLanguageFromCookie != null)
    //            {
    //                cultureName = currentLanguageFromCookie.Value;
    //                CultureHelper.SetCulture(cultureName);
    //                newUrl = baseurl + cultureName;
    //                filterContext.Result = new RedirectResult(newUrl);
    //                return;
    //            }

    //            if (currentLanguageFromCulture != null)
    //            {
    //                cultureName = currentLanguageFromCulture;
    //                CultureHelper.SetCulture(cultureName);
    //                newUrl = baseurl + cultureName;
    //                filterContext.Result = new RedirectResult(newUrl);
    //                return;
    //            }

    //            cultureName = possibleCultures[0];
    //            CultureHelper.SetCulture(cultureName);
    //            newUrl = baseurl + cultureName;
    //            filterContext.Result = new RedirectResult(newUrl);
    //            return;
    //        }

    //        CultureHelper.SetCulture(currentLanguageFromUrl);

    //        base.OnActionExecuting(filterContext);
    //    }
    //}
}
