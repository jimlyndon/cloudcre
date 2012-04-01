using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cloudcre.Model;
using Cloudcre.Report.Summary;

namespace Cloudcre.Report.Tests
{
    [TestClass]
    public class Summary
    {
        [TestMethod]
        public void CanCreatePackage()
        {
            string path = @"C:\Whitney\whitneycomp\TestResults\Report.xlsx";
            var propertyViewModels = new List<PropertyViewModel>
            {
                GetPropertyViewModelFor("FooApartments", DateTime.Today),
                GetPropertyViewModelFor("BarApartments", DateTime.Today),
                GetPropertyViewModelFor("BocaApartments", DateTime.Today),
                GetPropertyViewModelFor("WhatApartments", DateTime.Today),
                GetPropertyViewModelFor("2010Apartments", DateTime.Today.AddYears(-1)),
                GetPropertyViewModelFor("Bar2010Apartments", DateTime.Today.AddYears(-1)),
                GetPropertyViewModelFor("Foo2010Apartments", DateTime.Today.AddYears(-1)),
                GetPropertyViewModelFor("2009partments", DateTime.Today.AddYears(-2)),
                GetPropertyViewModelFor("2009FooApartments", DateTime.Today.AddYears(-2)),
                GetPropertyViewModelFor("2009BarApartments", DateTime.Today.AddYears(-2)),
                GetPropertyViewModelFor("2008Apartments", DateTime.Today.AddYears(-3)),
                GetPropertyViewModelFor("2008FooApartments", DateTime.Today.AddYears(-3)),
                GetPropertyViewModelFor("ListApartments", DateTime.Today.AddYears(-1), true),
                GetPropertyViewModelFor("FooListApartments", DateTime.Today.AddYears(-1), true),
                GetPropertyViewModelFor("BarListApartments", DateTime.Today.AddYears(-2), true)
            };

            var workbook = new SummaryReport();
            workbook.CreatePackage(path, propertyViewModels);

        }

        private static PropertyViewModel GetPropertyViewModelFor(String name, DateTime dateTime, bool isListed = false)
        {
            return new PropertyViewModel
                       {
                           Name = name,
                           AverageSquareFootPerUnit = 222.40M,
                           BedroomMix = "Lorem ipsum dolor sit amet",
                           BuiltTimePeriod = "1972 (renov 2003)",
                           Class = "Lorem ipsum dolor",
                           Condition = "Lorem ipsum dolor",
                           Quality = "Lorem ipsum dolor",
                           Stories = 3,
                           BuildingTotalSquareFoot = 325424.34M,
                           Units = 35,
                           EffectiveGrossIncome = 111111.34M,
                           GrossRentMultiplier = 11111.23M,
                           NetOperatingIncome = 123111M,
                           Occupancy = 11.0M,
                           OperatingExpense = 111111.3M,
                           OverallRate = 11.5M,
                           PotentialGrossIncome = 1111.32M,
                           ContractPeriod = "Lorem ipsum dolor sit amet",
                           CostPerBuildingSquareFoot = 111.23M,
                           CostPerUnit = 1111.32M,
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
                           Acres = 11M,
                           BuildingAreaRatio = .11M,
                           CensusTract = "Lorem ipsum dolor sit amet",
                           FloodZone = "Lorem ipsum dolor sit amet",
                           FloorToAreaRatio = .11M,
                           LegalDescription = "Lorem ipsum dolor sit amet",
                           Parking = "Good", //Property.ParkingType.Good,
                           ParcelId = "Lorem ipsum dolor sit amet",
                           SiteTotalSquareFoot = 55111M,
                           Zoning = "CA",
                           Latitude = 25.7738889M,
                           Longitude = -80.1938889M,
                           Address = new AddressViewModel
                                         {
                                             AddressLine2 = string.Empty,
                                             AddressLine1 = "Lorem ipsum",
                                             City = "Lorem ipsum dolor sit amet",
                                             County = "Lorem ipsum dolor sit amet",
                                             MetropolitanStatisticalArea = "Lorem ipsum dolor sit amet",
                                             StateProvinceRegion = "Lorem ipsum dolor sit amet",
                                             Zip = "111111"
                                         },
                       };
        }
    }
}
