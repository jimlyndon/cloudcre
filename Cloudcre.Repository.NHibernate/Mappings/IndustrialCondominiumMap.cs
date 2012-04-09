using Cloudcre.Model;
using FluentNHibernate.Mapping;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class IndustrialCondominiumMap : SubclassMap<IndustrialCondominium>
    {
        public IndustrialCondominiumMap()
        {
            Map(x => x.SingleUnitCondition);
            Map(x => x.SingleUnitFinish);
            Map(x => x.SingleUnitSquareFoot);
            Map(x => x.SingleUnitUse);
            Map(x => x.WareHouseAccess);
            Map(x => x.WareHouseClearHeight);

            DiscriminatorValue((int)PropertyType.IndustrialCondominium);
        }
    }
}