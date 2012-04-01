using System;
using System.Collections.Generic;

namespace Cloudcre.Service.Messages
{
    public class GetPropertiesRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}