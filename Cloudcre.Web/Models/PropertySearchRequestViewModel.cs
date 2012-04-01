using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cloudcre.Model;
using Cloudcre.Service.Messages;

namespace Cloudcre.Web.Models
{
    [DataContract]
    public class PropertySearchRequestViewModel
    {
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public string Query { get; set; }
        [DataMember]
        public IEnumerable<Location> Location { get; set; }
        [DataMember]
        public PropertiesSortBy SortBy { get; set; }
        [DataMember]
        public PropertyType ProperyTypeFilter { get; set; }
        [DataMember]
        public decimal? SqftMaxFilter { get; set; }
        [DataMember]
        public decimal? SqftMinFilter { get; set; }
        [DataMember]
        public DateTime? MinimumDateFilter { get; set; }
        [DataMember]
        public DateTime? MaximumDateFilter { get; set; }
        [DataMember]
        public MapBoundary MapBoundary { get; set; }
    }

    [DataContract]
    public class MapBoundary
    {
        [DataMember]
        public LngLatBoundary NorthEast { get; set; }
        [DataMember]
        public LngLatBoundary SouthWest { get; set; }
    }

    [DataContract]
    public class LngLatBoundary
    {
        [DataMember]
        public decimal? Latitude { get; set; }
        [DataMember]
        public decimal? Longitude { get; set; }
    }

    [DataContract]
    public class Location
    {
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Query { get; set; }
    }
}