using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Service;
using Cloudcre.Service.Messages;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Controllers
{    
    public class ReportController : BaseController
    {
        private readonly ReportingService _reportingSaleService;
        private readonly ICookieStorageService _cookieStorageService;

        public ReportController(ICookieStorageService cookieStorageService, ReportingService reportingSaleService)
            : base(cookieStorageService)
        {
            _cookieStorageService = cookieStorageService;
            _reportingSaleService = reportingSaleService;            
        }

        //
        // POST: /Report/Summary

        [HttpPost]
        public FileResult Summary()
        {
            var request = new GetReportRequest();
            var queue = _cookieStorageService.Retrieve<QueueViewModel>("ws");

            request.ParcelIds = queue.QueuedItems.Select(x => x.ParcelId);

            var response = _reportingSaleService.SummaryReport(request);
            if (!response.Success)
            {
                TempData["message"] = response.Message;
                return null;
            }

            return File(response.Report, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "SummaryReport.xlsx");
        }               
    }
}