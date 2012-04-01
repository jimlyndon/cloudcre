using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;

namespace Cloudcre.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICookieStorageService _cookieStorageService;

        public BaseController(ICookieStorageService cookieStorageService)
        {
            _cookieStorageService = cookieStorageService;
        }        
    }
}
