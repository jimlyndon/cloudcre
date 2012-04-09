using Cloudcre.Model;
using FluentNHibernate.Mapping;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class RetailMap : SubclassMap<Retail>
    {
        public RetailMap()
        {
            Map(x => x.AverageAnnualDailyTraffic);

            DiscriminatorValue((int)PropertyType.Retail);
        }
    }
}