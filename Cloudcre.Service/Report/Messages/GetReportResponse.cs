using System.IO;
using Cloudcre.Service.Messages;

namespace Cloudcre.Service.Report.Messages
{
    public class GetReportResponse : ResponseBase
    {
        public MemoryStream Report { get; set; }

        public string ReportName { get; set; }                            
    }
}