using System;
using System.Collections.Generic;

namespace Cloudcre.Service.Report.Messages
{
    public class GetReportRequest
    {
        public IEnumerable<string> ParcelIds { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
    }
}