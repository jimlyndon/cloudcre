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
    public class ResidentialLandController : PropertyBaseController<ResidentialLand, Guid, ResidentialLandViewModel>
    {
        private readonly ResidentialLandService _service;

        public ResidentialLandController(ICookieStorageService cookieStorageService, PropertyControllerConfigurationProvider configurationProvider, ResidentialLandService service)
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
            return View(new ResidentialLandViewModel());
        }

        [HttpGet]
        public ActionResult Model(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                GetPropertyResponse<ResidentialLandViewModel> response = _service.GetProperty(new GetPropertyRequest<Guid> { Id = id.Value });

                return Json(response.ViewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(new ResidentialLandViewModel(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                return View(new ResidentialLandViewModel { Id = id.Value });
            }

            return View(new ResidentialLandViewModel());
        }
    }
}