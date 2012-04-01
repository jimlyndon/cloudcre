using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Cloudcre.Model;
using Cloudcre.Service.Messages;
using PropertyType = Cloudcre.Model.PropertyType;

namespace Cloudcre.Service.Tests
{
    [TestClass]
    public class ReportingServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Report.Summary.AutoMapperBootStrapper.ConfigureAutoMapper();
        }
        
        [TestMethod]
        public void CanCreateSummaryReportWithNoPropertyRecords()
        {
            const string reportName = "CanCreateSummaryReportWithNoPropertyRecords";

            // Arrange: Given a property repository with no property records
            var models = new List<MultipleFamily>();
            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header and footer/legend
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOnePropertyRecordFromCurrentYear()
        {
            const string reportName = "CanCreateSummaryReportWithOnePropertyRecordFromCurrentYear";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today)
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user requests a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend and one record from current year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentYear()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentYear";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today),
                GetModelFor("BarApartments", DateTime.Today),
                GetModelFor("BasApartments", DateTime.Today),
                GetModelFor("BatApartments", DateTime.Today),
                GetModelFor("BazApartments", DateTime.Today)
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user requests a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend and several records from current year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOnePropertyRecordFromPriorYear()
        {
            const string reportName = "CanCreateSummaryReportWithOnePropertyRecordFromPriorYear";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today.AddYears(-1))                
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, prior year subheader and one record from prior year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromPriorYear()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromPriorYear";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BarApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BasApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BatApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BazApartments", DateTime.Today.AddYears(-1)),
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, prior year subheader and several records from prior year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOnePropertyRecordFromCurrentAndPriorYears()
        {
            const string reportName = "CanCreateSummaryReportWithOnePropertyRecordFromCurrentAndPriorYears";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today.AddDays(-1)),
                GetModelFor("BarApartments", DateTime.Today.AddDays(-5).AddYears(-1))
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, one record from current year, 
            // prior year subheader and one record from prior year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorYears()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorYears";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today),
                GetModelFor("BarApartments", DateTime.Today),
                GetModelFor("BasApartments", DateTime.Today),
                GetModelFor("BatApartments", DateTime.Today),
                GetModelFor("BazApartments", DateTime.Today),
                GetModelFor("FoooApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BaroApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BasoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BatoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BazoApartments", DateTime.Today.AddYears(-1))
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, several records from current 
            // and prior years, and prior year subheader
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOnePropertyRecordFromPreviousYears()
        {
            const string reportName = "CanCreateSummaryReportWithOnePropertyRecordFromPreviousYears";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today.AddYears(-2))                
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, prior year subheader, footer/legend, specific previous 
            // year subheader, and one record from a previous year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromPreviousYears()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromPreviousYears";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BarApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BasApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BatApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BazApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("FoooApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BaroApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BasoApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BatoApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BazoApartments", DateTime.Today.AddYears(-6))                 
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, prior year subheader, specific previous 
            // year subheader, and several records from previous years
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOnePropertyRecordFromCurrentAndPriorAndPreviousYears()
        {
            const string reportName = "CanCreateSummaryReportWithOnePropertyRecordFromCurrentAndPriorAndPreviousYears";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today),
                GetModelFor("BarApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("FooApartments", DateTime.Today.AddYears(-2))
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, one record from current year, prior year 
            // subheader, one record from prior year, previous year subheader and one record from previous year
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorAndPreviousYears()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorAndPreviousYears";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today),
                GetModelFor("BarApartments", DateTime.Today),
                GetModelFor("BasApartments", DateTime.Today),
                GetModelFor("BatApartments", DateTime.Today),
                GetModelFor("BazApartments", DateTime.Today),
                GetModelFor("FoooApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BaroApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BasoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BatoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BazoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("FooqApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BarqApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BasqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BatqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BazqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("FoooqApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BaroqApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BasoqApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BatoqApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BazoqApartments", DateTime.Today.AddYears(-6)) 
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, prior year subheader, previous records 
            // subheaders, and several records from current, prior and previous years
        }

        [TestMethod]
        public void CanCreateSummaryReportWithOneListedPropertyRecord()
        {
            const string reportName = "CanCreateSummaryReportWithOneListedPropertyRecord";

            // Arrange: Given a property repository
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today, true)                
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, listing subheader and one listed record
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralListedPropertyRecords()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralListedPropertyRecords";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today, true),
                GetModelFor("BarApartments", DateTime.Today, true),
                GetModelFor("FoooApartments", DateTime.Today.AddYears(-1), true),
                GetModelFor("BaroApartments", DateTime.Today.AddYears(-1), true),
                GetModelFor("BasoApartments", DateTime.Today.AddYears(-1), true),
                GetModelFor("FooApartments", DateTime.Today.AddYears(-2), true),
                GetModelFor("BarApartments", DateTime.Today.AddYears(-2),true)
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, listing subheader and several listed records
        }

        [TestMethod]
        public void CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorAndPreviousYearsAndSeveralListedPropertyRecords()
        {
            const string reportName = "CanCreateSummaryReportWithSeveralPropertyRecordsFromCurrentAndPriorAndPreviousYearsAndSeveralListedPropertyRecords";

            // Arrange: Given a repository with no property records
            var models = new List<MultipleFamily>
            {
                GetModelFor("FooApartments", DateTime.Today),
                GetModelFor("BarApartments", DateTime.Today),
                GetModelFor("BasApartments", DateTime.Today),
                GetModelFor("BatApartments", DateTime.Today),
                GetModelFor("BazApartments", DateTime.Today),
                GetModelFor("FoooApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BaroApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BasoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BatoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("BazoApartments", DateTime.Today.AddYears(-1)),
                GetModelFor("FooqApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BarqApartments", DateTime.Today.AddYears(-2)),
                GetModelFor("BasqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BatqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("BazqApartments", DateTime.Today.AddYears(-3)),
                GetModelFor("FoooqApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BaroqApartments", DateTime.Today.AddYears(-4)),
                GetModelFor("BasoqApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BatoqApartments", DateTime.Today.AddYears(-5)),
                GetModelFor("BazoqApartments", DateTime.Today.AddYears(-6)),
                GetModelFor("BataApartments", DateTime.Today, true),
                GetModelFor("BazaApartments", DateTime.Today, true),
                GetModelFor("FoooaApartments", DateTime.Today.AddYears(-1), true),
                GetModelFor("BaroaApartments", DateTime.Today.AddYears(-1), true),
                GetModelFor("BarzApartments", DateTime.Today.AddYears(-2), true),
                GetModelFor("BaszApartments", DateTime.Today.AddYears(-3), true),
                GetModelFor("BarozApartments", DateTime.Today.AddYears(-4), true),
                GetModelFor("BasozApartments", DateTime.Today.AddYears(-4), true),
                GetModelFor("BatozApartments", DateTime.Today.AddYears(-4), true)
            };

            var reportingService = new ReportingService(MockPropertyRepository(models.ToArray()));

            // Act: When a user request a report for the following properties (based on parcel ids)
            var request = new GetReportRequest { ParcelIds = new[] { "1111-1111" } };
            GetReportResponse response = reportingService.SummaryReport(request);
            RecreateReport(response.Report, reportName);

            // Assert: 
            // Manually check report that it was created and contains no errors when opened.
            // Should only contain main header, footer/legend, prior year subheader, previous records 
            // subheaders, several records from current, prior and previous years, and several listed records
        }

        private static void RecreateReport(MemoryStream reportStream, string reportName)
        {
            string fileName = String.Format(@"C:\Whitney\whitneycomp\TestResults\{0}.xlsx", reportName);
            try
            {
                File.Delete(fileName);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            byte[] data = reportStream.ToArray();
            var wFile = new FileStream(fileName, FileMode.Append);
            wFile.Write(data, 0, data.Length);
            wFile.Close();
        }

        private static IMultipleFamilyRepository MockPropertyRepository(params MultipleFamily[] prods)
        {
            // Generate an implementer of IProductsRepository at runtime using Moq
            var mockProductsRepos = new Mock<IMultipleFamilyRepository>();
            mockProductsRepos.Setup(x => x.FindAll()).Returns(prods.AsQueryable());
            return mockProductsRepos.Object;
        }

        private static MultipleFamily GetModelFor(String name, DateTime dateTime, bool isListed = false)
        {
            return new MultipleFamily
                       {
                           Name = name,
                           Id = Guid.NewGuid(),
                           PropertyType = PropertyType.MultipleFamily,
                           BedroomMix = "Lorem ipsum dolor sit amet",
                           BuiltTimePeriod = "1972 (renov 2003)",
                           Class = "ipsum dolor",
                           Condition = "ipsum dolor",
                           Quality = "ipsum dolor",
                           Stories = 3,
                           BuildingTotalSquareFoot = 325424.34M,
                           Units = 35,
                           EffectiveGrossIncome = 234M,
                           GrossRentMultiplier = 11111.23M,
                           NetOperatingIncome = 123111M,
                           Occupancy = 11.0M,
                           OperatingExpense = 111111.3M,
                           PotentialGrossIncome = 1111.32M,
                           ContractPeriod = "Lorem ipsum dolor sit amet",
                           SaleDate = dateTime,
                           Financing = "Lorem ipsum dolor sit amet",
                           Grantee = "660 North Palm Beach, LLC",
                           Grantor = "FDIC",
                           ListedPriceAtSale = "Lorem ipsum dolor sit amet",
                           Price = 1750000M,
                           MarketingPeriod = "Lorem ipsum dolor sit amet",
                           OfficialRecordBookAndPage = "Lorem ipsum dolor sit amet",
                           PriorSales = "Lorem ipsum dolor sit amet",
                           PropertyRights = "Lorem ipsum dolor sit amet",
                           Verification = "Lorem ipsum dolor sit amet",
                           BuildingAreaRatio = .11M,
                           CensusTract = "Lorem ipsum dolor sit amet",
                           FloodZone = "Lorem ipsum dolor sit amet",
                           LegalDescription = "Lorem ipsum dolor sit amet",
                           Parking = "Lorem ipsum",
                           ParcelId = "1111-1111",
                           SiteTotalSquareFoot = 55111M,
                           Zoning = "CA",
                           Latitude = 25.7738889M,
                           Longitude = -80.1938889M,

                           Address = new Address
                                         {
                                         //    Id = Guid.NewGuid(),
                                             AddressLine2 = string.Empty,
                                             AddressLine1 = "Lorem ipsum",
                                             City = "Lorem ipsum dolor sit amet",
                                             County = "Lorem ipsum dolor sit amet",
                                             MetropolitanStatisticalArea = "Lorem ipsum dolor sit amet",
                                             StateProvinceRegion = "Lorem ipsum dolor sit amet",
                                             Zip = "111111"
                                         }
                       };
        }
    }
}