using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class BuildingPropertyMap : SubclassMap<BuildingProperty>
    {
        public BuildingPropertyMap()
        {
            Map(x => x.BuildingAreaRatio);
            Map(x => x.BuildingTotalSquareFoot);
            Map(x => x.BuiltTimePeriod);
        }
    }
}