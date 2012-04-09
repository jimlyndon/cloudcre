using Cloudcre.Model;
using FluentNHibernate.Mapping;

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