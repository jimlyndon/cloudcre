using System.Collections.Generic;
using Cloudcre.Service.Messages;

namespace Cloudcre.Service.Property.Messages
{
    public class GetPropertiesResponse<TVm> : ResponseBase
    {
        public IEnumerable<TVm> ViewModels { get; set; }
    }
}