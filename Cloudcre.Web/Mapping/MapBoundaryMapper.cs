using AutoMapper;
using Cloudcre.Service.Messages;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Mapping
{
    public static class MapBoundaryMapper
    {
        public static SearchPropertyRequest.MapBoundary ConvertToSearchPropertyRequestMapBoundary(this MapBoundary obj)
        {
            return Mapper.Map<MapBoundary, SearchPropertyRequest.MapBoundary>(obj);
        }
    }
}