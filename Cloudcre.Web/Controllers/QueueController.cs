using System;
using System.Linq;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Service;
using Cloudcre.Service.Messages;
using Cloudcre.Service.ViewModels;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Controllers
{
    public class QueueController : BaseController
    {
        private readonly MultipleFamilyService _multipleFamilyService;
        private readonly ICookieStorageService _cookieStorageService;

        public QueueController(ICookieStorageService cookieStorageService,
            MultipleFamilyService multipleFamilyService)
            : base(cookieStorageService)
        {
            _cookieStorageService = cookieStorageService;
            _multipleFamilyService = multipleFamilyService;            
        }

        //
        // GET: /Queue/

        public ActionResult Index()
        {
            var request = new GetPropertiesRequest();

            var queue = _cookieStorageService.Retrieve<QueueViewModel>("ws");

            if (queue != null)
            {
                request.Ids = queue.QueuedItems.Select(x => x.Id);
            }

            GetPropertiesResponse<MultipleFamilyViewModel> response = _multipleFamilyService.GetProperties(request);

            if (!response.Success)
            {
                TempData["message"] = response.Message;
                return View();
            }

            QueueResultViewModel viewModel = GetQueueResultViewFrom(response);

            return View(viewModel);
        }

        private QueueResultViewModel GetQueueResultViewFrom(GetPropertiesResponse<MultipleFamilyViewModel> response)
        {
            return new QueueResultViewModel
            {
                Properties = response.ViewModels
            };
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                RemoveFromQueue(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult JsonDelete(Guid id)
        {
            try
            {
                RemoveFromQueue(id);

                var request = new GetPropertiesRequest();
                var queue = _cookieStorageService.Retrieve<QueueViewModel>("ws");

                request.Ids = queue.QueuedItems.Where(x => x.Id != id).Select(x => x.Id);

                GetPropertiesResponse<MultipleFamilyViewModel> response = _multipleFamilyService.GetProperties(request);

                if (!response.Success)
                {
                    TempData["message"] = response.Message;
                    return Json(TempData["message"]);
                }

                QueueResultViewModel viewModel = GetQueueResultViewFrom(response);

                return Json(viewModel);
            }
            catch
            {
                return Json(null);
            }
        }

        private void RemoveFromQueue(Guid id)
        {
            var queue = _cookieStorageService.Retrieve<QueueViewModel>("ws");
            queue.QueuedItems = queue.QueuedItems.Where(x => x.Id != id);
            _cookieStorageService.Save("ws", queue);
        }       
    }
}