using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Model;
using Cloudcre.Service.Property;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Report.Messages;
using Cloudcre.Web.Configuration;
using Cloudcre.Web.HtmlHelpers;
using Cloudcre.Web.Mapping;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Controllers
{
    public class PropertyBaseController<T, TId, TVm> : BaseController
        where T : Property
        where TId : struct
    {
        private readonly IPropertyBaseService<T, TId, TVm> _service;
        private readonly PropertyControllerConfigurationProvider _configurationProvider;
        private const int DefaultResultsPerPage = 10;

        public PropertyBaseController(IPropertyBaseService<T, TId, TVm> service, PropertyControllerConfigurationProvider configurationProvider, ICookieStorageService cookieStorageService)
            : base(cookieStorageService)
        {
            _service = service;
            _configurationProvider = configurationProvider;
        }

        // uncomment to allow creation or editing of properties
        //[HttpPost]
        //public ActionResult Create(TVm viewModel)
        //{
        //    return CreateOrEdit(viewModel);
        //}
        //
        //[HttpPost]
        //public ActionResult Edit(TVm viewModel)
        //{
        //    return CreateOrEdit(viewModel);
        //}
        //
        //private ActionResult CreateOrEdit(TVm viewModel)
        //{
        //    if (ModelState.IsValid )
        //    {
        //        var request = new AddPropertyRequest<TVm> { ViewModel = viewModel };
        //        AddPropertyResponse response = _service.AddProperty(request);
        //    }
        //
        //    return Json(new { });
        //}

        // uncomment to enable deletion of properties
        //[HttpPost]
        //public ActionResult Delete(TId id)
        //{
        //    RemovePropertyResponse response = _service.RemoveProperty(new RemovePropertyRequest<TId>
        //    {
        //        Id = id
        //    });

        //    if (!response.Success)
        //    {
        //        TempData["message"] = response.Message;
        //    }

        //    //return new HttpStatusCodeResult(200);
        //    return new EmptyResult();
        //}

        [HttpPost]
        public ActionResult Summary(IEnumerable<Guid> ids)
        {
            if (ModelState.IsValid)
            {
                var request = new GetReportRequest { Ids = ids };

                var response = _service.SummaryReport(request);
                if (!response.Success)
                {
                    TempData["message"] = response.Message;
                    return null;
                }

                return File(response.Report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.ReportName);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        public JsonResult Search(PropertySearchRequestViewModel propertySearchRequestViewModel)
        {
            SearchPropertyRequest request = GenerateSearchPropertiesRequestFrom(propertySearchRequestViewModel);
            SearchPropertyResponse<TVm> propertyResponse = _service.SearchProperties(request);

            if (!propertyResponse.Success)
            {
                TempData["message"] = propertyResponse.Message;
                return Json(TempData["message"]);
            }

            return Json(GetPropertiesSearchResultViewFrom(propertyResponse));
        }

        private SearchResultViewModel<TVm> GetPropertiesSearchResultViewFrom(SearchPropertyResponse<TVm> propertyResponse)
        {
            return new SearchResultViewModel<TVm>
            {
                CurrentPage = propertyResponse.CurrentPage,
                NumberOfTitlesFound = propertyResponse.NumberOfTitlesFound,
                Properties = propertyResponse.Properties,
                TotalNumberOfPages = propertyResponse.TotalNumberOfPages,
                SqftMaxFilter = propertyResponse.SqftMaxFilter,
                SqftMinFilter = propertyResponse.SqftMinFilter,
                SqftMax = propertyResponse.SqftMax,
                SqftMin = propertyResponse.SqftMin,
                PropertyType = propertyResponse.PropertyType.ToSelectList()
            };
        }

        private SearchPropertyRequest GenerateSearchPropertiesRequestFrom(PropertySearchRequestViewModel propertySearchRequestViewModel)
        {
            var request = new SearchPropertyRequest
            {
                NumberOfResultsPerPage = string.IsNullOrEmpty(_configurationProvider.NumberOfResultsPerPage)
                        ? DefaultResultsPerPage
                        : int.Parse(_configurationProvider.NumberOfResultsPerPage),
                PropertyTypeFilter = propertySearchRequestViewModel.ProperyTypeFilter,
                Query = string.IsNullOrEmpty(propertySearchRequestViewModel.Query)
                        ? string.Empty
                        : propertySearchRequestViewModel.Query.Trim(),
                LocationQueries = propertySearchRequestViewModel.Location.ConvertToSearchPropertyRequestLocation(),
                Index = propertySearchRequestViewModel.Index,
                SortBy = propertySearchRequestViewModel.SortBy,
                SqftMaxFilter = propertySearchRequestViewModel.SqftMaxFilter,
                SqftMinFilter = propertySearchRequestViewModel.SqftMinFilter,
                MaximumDateFilter = propertySearchRequestViewModel.MaximumDateFilter,
                MinimumDateFilter = propertySearchRequestViewModel.MinimumDateFilter,
                MappingBoundary = propertySearchRequestViewModel.MapBoundary.ConvertToSearchPropertyRequestMapBoundary()
            };

            return request;
        }
    }
}