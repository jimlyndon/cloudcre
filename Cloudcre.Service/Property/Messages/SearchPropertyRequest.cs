using System;
using System.Collections.Generic;
using Cloudcre.Model;

namespace Cloudcre.Service.Property.Messages
{
    public class SearchPropertyRequest
    {
        public string Query { get; set; }
        public IEnumerable<Location> LocationQueries { get; set; }
        public PropertyType PropertyTypeFilter { get; set; }
        public int Index { get; set; }
        public int NumberOfResultsPerPage { get; set; }
        public PropertiesSortBy SortBy { get; set; }
        public decimal? SqftMaxFilter { get; set; }
        public decimal? SqftMinFilter { get; set; }
        public DateTime? MinimumDateFilter { get; set; }
        public DateTime? MaximumDateFilter { get; set; }
        public MapBoundary MappingBoundary { get; set; }

        public class MapBoundary
        {
            public LngLatBoundary NorthEast { get; set; }
            public LngLatBoundary SouthWest { get; set; }
        }

        public class LngLatBoundary
        {
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
        }

        public class Location
        {
            public string Category { get; set; }
            public string Query { get; set; }
        }
    }
}