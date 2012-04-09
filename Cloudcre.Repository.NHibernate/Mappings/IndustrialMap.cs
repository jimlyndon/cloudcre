using Cloudcre.Model;
using FluentNHibernate.Mapping;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class IndustrialMap : SubclassMap<Industrial>
    {
        public IndustrialMap()
        {
            Map(x => x.ConstructionMaterial);
            Map(x => x.PercentOfBuildingFinished);
            Map(x => x.WareHouseAccess);
            Map(x => x.WareHouseClearHeight);

            DiscriminatorValue((int)PropertyType.Industrial);
        }
    }
}