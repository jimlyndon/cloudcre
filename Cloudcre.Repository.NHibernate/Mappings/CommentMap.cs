using Cloudcre.Model;
using FluentNHibernate.Mapping;

namespace Cloudcre.Repository.NHibernate.Mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Text);
            //References(x => x.Property).Column("PropertyId");
        }
    }
}