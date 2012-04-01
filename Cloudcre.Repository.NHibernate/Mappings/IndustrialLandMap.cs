using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class IndustrialLandMap : SubclassMap<IndustrialLand>
    {
        public IndustrialLandMap()
        {
            Map(x => x.Approvals);
            Map(x => x.FrontageToDepth);
            Map(x => x.FutureLandUse);
            Map(x => x.PlannedUse);
            Map(x => x.ProposedSize);
            Map(x => x.Surface);
            Map(x => x.Utilities);
            Map(x => x.Visibility);

            DiscriminatorValue((int)PropertyType.IndustrialLand);
        }
    }
}