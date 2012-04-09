using System.Collections.Generic;
using Cloudcre.Service.Messages;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Service.Property.Messages
{        
    public class LocationsResponse : ResponseBase
    {
        public List<LocationViewModel> Locations { get; set; }
    }
}