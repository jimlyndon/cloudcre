using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class MultipleFamilyMap : SubclassMap<MultipleFamily>
    {
        public MultipleFamilyMap()
        {
            Map(x => x.BedroomMix);
            Map(x => x.Units);

            DiscriminatorValue((int)PropertyType.MultipleFamily);
        }
    }
}