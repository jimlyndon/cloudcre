using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class ResidentialLandMap : SubclassMap<ResidentialLand>
    {
        public ResidentialLandMap()
        {
            Map(x => x.Approvals);
            Map(x => x.Density);
            Map(x => x.FutureLandUse);
            Map(x => x.PlannedUse);
            Map(x => x.Surface);
            Map(x => x.Units);
            Map(x => x.Utilities);

            DiscriminatorValue((int)PropertyType.ResidentialLand);
        }
    }
}