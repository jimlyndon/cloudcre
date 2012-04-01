using System.Collections.Generic;

namespace Cloudcre.Service.Messages
{
    public class GetReportRequest
    {
        public IEnumerable<string> ParcelIds { get; set; }
    }
}