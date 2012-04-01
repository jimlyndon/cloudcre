using System;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Model;
using Cloudcre.Service;
using Cloudcre.Service.Messages;
using Cloudcre.Service.ViewModels;
using Cloudcre.Web.Configuration;

namespace Cloudcre.Web.Controllers
{
    public class OfficeController : PropertyBaseController<Office, Guid, OfficeViewModel>
    {
        private readonly OfficeService _service;

        public OfficeController(ICookieStorageService cookieStorageService, PropertyControllerConfigurationProvider configurationProvider, OfficeService service)
            : base(service, configurationProvider, cookieStorageService)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (configurationProvider == null)
                throw new ArgumentNullException("configurationProvider");

            _service = service;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new OfficeViewModel());
        }

        [HttpGet]
        public ActionResult Model(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                GetPropertyResponse<OfficeViewModel> response = _service.GetProperty(new GetPropertyRequest<Guid> { Id = id.Value });

                return Json(response.ViewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(new OfficeViewModel(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                return View(new OfficeViewModel { Id = id.Value });
            }

            return View(new OfficeViewModel());
        }
    }
}