using System;
using System.Web.Mvc;

namespace Cloudcre.Web.Filters
{
    public class RequireHttpsExceptLocalhostAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request == null)
            {
                throw new ApplicationException("Request is undefined");
            }

            var uriBuilder = new UriBuilder(filterContext.HttpContext.Request.Url);

            if (!uriBuilder.Host.ToLower().Contains("localhost"))
            {
                base.HandleNonHttpsRequest(filterContext);
            }
        }
    }
}