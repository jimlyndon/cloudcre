using Cloudcre.Model;
using FluentNHibernate.Mapping;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class CommercialLandMap : SubclassMap<CommercialLand>
    {
        public CommercialLandMap()
        {
            Map(x => x.Approvals);
            Map(x => x.AverageAnnualDailyTraffic);
            Map(x => x.FrontageToDepth);
            Map(x => x.FrontFt);
            Map(x => x.FutureLandUse);
            Map(x => x.PlannedUse);
            Map(x => x.ProposedSize);
            Map(x => x.Surface);
            Map(x => x.Utilities);
            Map(x => x.Visibility);

            DiscriminatorValue((int)PropertyType.CommercialLand);
        }
    }
}