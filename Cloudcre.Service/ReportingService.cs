using System.IO;
using System.Linq;
using Cloudcre.Model;
using Cloudcre.Report.Summary;
using Cloudcre.Service.Messages;
using System.Collections.Generic;

namespace Cloudcre.Service
{
    public class ReportingService
    {
        private readonly IMultipleFamilyRepository _multipleFamilyRepository;

        public ReportingService(IMultipleFamilyRepository multipleFamilyRepository)
        {
            _multipleFamilyRepository = multipleFamilyRepository;
        }

        public GetReportResponse SummaryReport(GetReportRequest request)
        {
            var response = new GetReportResponse();

            if (request.ParcelIds == null)
            {
                response.Success = false;
                return response;
            }

            var properties = from propertyRepo in _multipleFamilyRepository.FindAll()
                             join parcelids in request.ParcelIds on propertyRepo.ParcelId equals parcelids
                             select propertyRepo;

            var propertViewModels = properties.ConvertToPropertyViews().OrderByDescending(x => x.SaleDate);

            var stream = new MemoryStream();
            var workbook = new SummaryReport();
            workbook.CreatePackage(stream, propertViewModels);
            stream.Seek(0, SeekOrigin.Begin);

            response.Report = stream;
            response.Success = true;

            return response;
        }
    }
}