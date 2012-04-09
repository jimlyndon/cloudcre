using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Web.Filters;
using Microsoft.Web.Mvc;

namespace Cloudcre.Web.Controllers
{
    [ValidateOnlyIncomingValues]
    public class WizardController<T> : BaseController where T : class, new()
    {
        protected T viewModel;

        public WizardController(ICookieStorageService cookieStorageService)
            : base(cookieStorageService)
        { }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var serialized = Request.Form["SerializedViewModel"];
            if (serialized != null)
            {
                // wizard form was posted containing serialized data
                viewModel = (T)new MvcSerializer().Deserialize(serialized, SerializationMode.EncryptedAndSigned);
                TryUpdateModel(viewModel);

            }
            else
                // wizard is new or is requested via redirect to action
                viewModel = (T)TempData["SerializedViewModel"] ?? new T();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult || filterContext.Result is PartialViewResult || filterContext.Result is JsonResult)
                TempData["SerializedViewModel"] = viewModel;
        }
    }
}