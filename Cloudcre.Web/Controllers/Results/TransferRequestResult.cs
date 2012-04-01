using System.Web;
using System.Web.Mvc;

namespace Cloudcre.Web.Controllers.Results
{
    public class TransferRequestResult : ActionResult
    {
        private readonly string _url;

        public TransferRequestResult(string url)
        {
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            // MVC 3 running on IIS 7+
            if (HttpRuntime.UsingIntegratedPipeline)
            {
                context.HttpContext.Server.TransferRequest(_url);
                //context.HttpContext.Server.TransferRequest(_url, true);
            }
            else
            {
                // Pre MVC 3
                context.HttpContext.RewritePath(_url, false);

                IHttpHandler httpHandler = new MvcHttpHandler();
                httpHandler.ProcessRequest(context.HttpContext.ApplicationInstance.Context);
            }
        }
    }
}