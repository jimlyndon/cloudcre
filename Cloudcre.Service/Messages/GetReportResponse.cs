using System.IO;

namespace Cloudcre.Service.Messages
{
    public class GetReportResponse : ResponseBase
    {
        public MemoryStream Report { get; set; }
    }
}