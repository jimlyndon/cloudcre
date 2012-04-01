using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class OfficeMap : SubclassMap<Office>
    {
        public OfficeMap()
        {
            DiscriminatorValue((int)PropertyType.Office);
        }
    }
}