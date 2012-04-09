using System;
using System.Collections.Generic;

namespace Cloudcre.Service.Property.Messages
{
    public class GetPropertiesRequest
    {
        public IEnumerable<Guid> Ids { get; set; }
    }
}