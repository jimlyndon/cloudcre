using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cloudcre.Web.Controllers;
using Cloudcre.Web.Controllers.Results;

namespace Cloudcre.Web.Filters
{
    public class AuthorizeAndTransferAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAccessAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAccessAttribute), true);
            if (!skipAuthorization)
            {
                base.OnAuthorization(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            string url = urlHelper.Action("LogOn", "Account");
            filterContext.Result = new TransferRequestResult(url);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAnonymousAccessAttribute : Attribute
    {
    }
}