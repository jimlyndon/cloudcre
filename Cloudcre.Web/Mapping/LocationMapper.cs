using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Web.Mapping
{
    public static class LocationMapper
    {
        public static IEnumerable<SearchPropertyRequest.Location> ConvertToSearchPropertyRequestLocation(this IEnumerable<Models.Location> obj)
        {
            return Mapper.Map<IEnumerable<Models.Location>, IEnumerable<SearchPropertyRequest.Location>>(obj);
        }

        public static IEnumerable<SelectListItem> ConvertToSelectListItemEnumerable(this IEnumerable<LocationViewModel> locationViewModel)
        {
            return Mapper.Map<IEnumerable<LocationViewModel>, IEnumerable<SelectListItem>>(locationViewModel);
        }
    }
}