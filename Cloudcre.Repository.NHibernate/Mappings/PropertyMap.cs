using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class PropertyMap : ClassMap<Property>
    {
        public PropertyMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Acres);
            Map(x => x.CensusTract);
            Map(x => x.Class);
            Map(x => x.Comments);
            Map(x => x.Condition).Column("`Condition`");
            Map(x => x.ContractDate);
            Map(x => x.ContractPeriod);
            Map(x => x.EffectiveGrossIncome);
            Map(x => x.Financing);
            Map(x => x.FloodZone);
            Map(x => x.Grantee);
            Map(x => x.Grantor);
            Map(x => x.GrossRentMultiplier);
            Map(x => x.Latitude);
            Map(x => x.LegalDescription);
            Map(x => x.ListedPriceAtSale);
            Map(x => x.ListingDate);
            Map(x => x.Longitude);
            Map(x => x.MarketingPeriod);
            Map(x => x.Name);
            Map(x => x.NetOperatingIncome);
            Map(x => x.Occupancy);
            Map(x => x.OfficialRecordBookAndPage);
            Map(x => x.OperatingExpense);
            Map(x => x.ParcelId);
            Map(x => x.Parking);
            //Map(x => x.Parking).CustomType(typeof(int));
            Map(x => x.PotentialGrossIncome);
            Map(x => x.Price);
            Map(x => x.PriorSales);
            Map(x => x.PropertyRights);
            Map(x => x.Quality);
            Map(x => x.SaleDate);//.CustomType<UtcDateTimeType>();
            Map(x => x.SiteTotalSquareFoot);
            Map(x => x.Stories);
            Map(x => x.Use).Column("`Use`");
            Map(x => x.Verification);
            Map(x => x.Zoning);

            Component(o => o.Address,
                      c =>
                          {
                              c.Map(r => r.AddressLine1);
                              c.Map(r => r.AddressLine2);
                              c.Map(r => r.City);
                              c.Map(r => r.County);
                              c.Map(r => r.MetropolitanStatisticalArea);
                              c.Map(r => r.StateProvinceRegion);
                              c.Map(r => r.Zip);
                          });

            DiscriminateSubClassesOnColumn("PropertyType");
        }
    }
}