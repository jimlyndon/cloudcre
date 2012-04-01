using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class CommercialCondominiumMap : SubclassMap<CommercialCondominium>
    {
        public CommercialCondominiumMap()
        {
            Map(x => x.SingleUnitCondition);
            Map(x => x.SingleUnitFinish);
            Map(x => x.SingleUnitFloor);
            Map(x => x.SingleUnitSquareFoot);
            Map(x => x.SingleUnitUse);

            DiscriminatorValue((int)PropertyType.CommercialCondominium);
        }
    }
}