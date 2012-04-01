using System.Collections.Generic;
using Cloudcre.Service.ViewModels;

namespace Cloudcre.Service.Messages
{        
    public class LocationsResponse : ResponseBase
    {
        public List<LocationViewModel> Locations { get; set; }
    }
}