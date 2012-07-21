using System;
using System.Data;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Repository.NHibernate;
using Cloudcre.Repository.NHibernate.Repositories;
using Cloudcre.Utilities.Console;
using NHibernate.Search;

namespace Cloudcre.Utilities.DataLoader.Marshallers
{
    public class PropertyMarshaller : CsvMarshaller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFullTextSession _session;

        public PropertyMarshaller(EnvironmentContext.Type environmentType)
        {
            var sessionfactorybuilder = new NHibernateSessionFactoryBuilder(EnvironmentContext.ConnectionString(environmentType), "~/LuceneIndex");

            _session = Search.CreateFullTextSession(sessionfactorybuilder.GetSessionFactory().OpenSession());

            _unitOfWork = new NHUnitOfWork(_session);
        }

        protected override void Marshall(DataTable table)
        {
            IMultipleFamilyRepository multipleFamilyRepository = new MultipleFamilyRepository(_unitOfWork, _session);

            for (int count = 0; count < table.Rows.Count; count++)
            {
                var apartment = new MultipleFamily
                {
                    Name = table.GetRowValue<string>("Name", count),
                    PropertyType = table.GetRowValue<PropertyType>("Name", count),
                    BedroomMix = table.GetRowValue<string>("BedroomMix", count),
                    BuiltTimePeriod = table.GetRowValue<string>("BuiltTimePeriod", count),
                    Class = table.GetRowValue<string>("Class", count),
                    Condition = table.GetRowValue<string>("Condition", count),
                    Quality = table.GetRowValue<string>("Quality", count),
                    Stories = table.GetRowValue<int?>("Stories", count),
                    BuildingTotalSquareFoot = (decimal?)table.GetRowValue<double?>("BuildingTotalSquareFoot", count),
                    Units = table.GetRowValue<int?>("Units", count),
                    EffectiveGrossIncome = (decimal?)table.GetRowValue<double?>("EffectiveGrossIncome", count),
                    GrossRentMultiplier = (decimal?)table.GetRowValue<double?>("GrossRentMultiplier", count),
                    NetOperatingIncome = (decimal?)table.GetRowValue<double?>("NetOperatingIncome", count),
                    Occupancy = (decimal?)table.GetRowValue<double?>("Occupancy", count),
                    OperatingExpense = (decimal?)table.GetRowValue<double?>("OperatingExpense", count),
                    PotentialGrossIncome = (decimal?)table.GetRowValue<double?>("PotentialGrossIncome", count),
                    ContractPeriod = table.GetRowValue<string>("ContractPeriod", count),
                    SaleDate = table.GetRowValue<DateTime>("Date", count),
                    Financing = table.GetRowValue<string>("Financing", count),
                    Grantee = table.GetRowValue<string>("Grantee", count),
                    Grantor = table.GetRowValue<string>("Grantor", count),
                    ListedPriceAtSale = table.GetRowValue<string>("ListedPriceAtSale", count),
                    Price = (decimal?) table.GetRowValue<double?>("Price", count),
                    MarketingPeriod = table.GetRowValue<string>("MarketingPeriod", count),
                    OfficialRecordBookAndPage = table.GetRowValue<string>("OfficialRecordBookAndPage", count),
                    PriorSales = table.GetRowValue<string>("PriorSales", count),
                    PropertyRights = table.GetRowValue<string>("PropertyRights", count),
                    Verification = table.GetRowValue<string>("Verification", count),
                    BuildingAreaRatio = (decimal?) table.GetRowValue<double?>("BuildingAreaRatio", count),
                    CensusTract = table.GetRowValue<string>("CensusTract", count),
                    FloodZone = table.GetRowValue<string>("FloodZone", count),
                    LegalDescription = table.GetRowValue<string>("LegalDescription", count),
                    Parking = table.GetRowValue<string>("Parking", count),
                    //Parking = table.GetRowValue<Property.ParkingType>("Parking", count),
                    ParcelId = table.GetRowValue<string>("ParcelId", count),
                    SiteTotalSquareFoot = (decimal?) table.GetRowValue<double?>("SiteTotalSquareFoot", count),
                    Zoning = table.GetRowValue<string>("Zoning", count),
                    Latitude = (decimal?) table.GetRowValue<double?>("Latitude", count),
                    Longitude = (decimal?) table.GetRowValue<double?>("Longitude", count),
                    Address = new Address
                    {
                        AddressLine1 = table.GetRowValue<string>("AddressLine1", count),
                        City = table.GetRowValue<string>("City", count),
                        County = table.GetRowValue<string>("County", count),
                        MetropolitanStatisticalArea = table.GetRowValue<string>("MetropolitanStatisticalArea", count),
                        StateProvinceRegion = table.GetRowValue<string>("StateProvinceRegion", count),
                        Zip = table.GetRowValue<string>("Zip", count)
                    }
                };

                multipleFamilyRepository.Add(apartment);
            }

            _unitOfWork.Commit();
            _session.Dispose();
        }
    }
}