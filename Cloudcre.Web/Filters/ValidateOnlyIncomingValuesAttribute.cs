using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cloudcre.Web.Filters
{
    public class ValidateOnlyIncomingValuesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ModelStateDictionary modelState = filterContext.Controller.ViewData.ModelState;

            IValueProvider incomingValues = filterContext.Controller.ValueProvider;

            IEnumerable<string> keys = modelState.Keys.Where(x => !incomingValues.ContainsPrefix(x));

            foreach (var key in keys) // foreach key that doesnt match any incoming value
                modelState[key].Errors.Clear(); // clear viewmodel errors to support partial validation
        }
    }
}