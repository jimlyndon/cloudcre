using System.Collections.Generic;

namespace Cloudcre.Service.Messages
{
    public class GetPropertiesResponse<TVm> : ResponseBase
    {
        public IEnumerable<TVm> ViewModels { get; set; }
    }
}