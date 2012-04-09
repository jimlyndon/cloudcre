using System;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Model;
using Cloudcre.Service.Property;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Web.Configuration;
using Cloudcre.Web.HtmlHelpers;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly PropertyControllerConfigurationProvider _configurationProvider;

        private readonly PropertyService _service;

        public SearchController(ICookieStorageService cookieStorageService,
            PropertyControllerConfigurationProvider configurationProvider,
            PropertyService service)
            : base(cookieStorageService)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException("configurationProvider");

            _configurationProvider = configurationProvider;

            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;
        }

        //
        // GET: /Property/

        public ActionResult Index()
        {
            PropertySearchResultViewModel propertySearchResultViewModel = GetPropertiesSearchResultViewFrom();

            return View(propertySearchResultViewModel);
        }

        [HttpPost]
        public JsonResult Locations()
        {
            var locationsRequest = new LocationsRequest
            {
                Term = string.Empty
            };

            LocationsResponse locationsResponse = _service.GetDistinctListofLocations(locationsRequest);

            return Json(locationsResponse.Locations);
        }

        private PropertySearchResultViewModel GetPropertiesSearchResultViewFrom()
        {
            return new PropertySearchResultViewModel
            {
                CurrentPage = 0,
                NumberOfTitlesFound = 0,
                TotalNumberOfPages = 0,
                SqftMaxFilter = 500000.0M,
                SqftMinFilter = 0M,
                SqftMax = 300000,
                SqftMin = 0,
                PropertyType = new PropertyType().ToSelectList()
            };
        }
    }
}