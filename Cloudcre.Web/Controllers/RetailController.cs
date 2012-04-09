using System;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Model;
using Cloudcre.Service.Property;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Property.ViewModels;
using Cloudcre.Web.Configuration;

namespace Cloudcre.Web.Controllers
{
    public class RetailController : PropertyBaseController<Retail, Guid, RetailViewModel>
    {
        private readonly RetailService _service;

        public RetailController(ICookieStorageService cookieStorageService, PropertyControllerConfigurationProvider configurationProvider, RetailService service)
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
            return View(new RetailViewModel());
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                return View(new RetailViewModel { Id = id.Value });
            }

            return View(new RetailViewModel());
        }

        [HttpGet]
        public ActionResult Model(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                GetPropertyResponse<RetailViewModel> response = _service.GetProperty(new GetPropertyRequest<Guid> { Id = id.Value });

                return Json(response.ViewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(new RetailViewModel(), JsonRequestBehavior.AllowGet);
        }
    }
}