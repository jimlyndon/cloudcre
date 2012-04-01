using FluentNHibernate.Mapping;
using Cloudcre.Model;

namespace Cloudcre.Repository.NHibernate.Mappings
{
   public class UserMap : ClassMap<User>
    {
       public UserMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Comments);
            Map(x => x.CreatedDate);
            Map(x => x.Email);
            Map(x => x.IsActivated);
            Map(x => x.IsLockedOut);
            Map(x => x.LastLockedOutDate);
            Map(x => x.LastLockedOutReason);
            Map(x => x.LastLoginDate);
            Map(x => x.LastLoginIp);
            Map(x => x.LastModifiedDate);
            Map(x => x.Name);
            Map(x => x.NewEmail);
            Map(x => x.NewEmailKey);
            Map(x => x.NewEmailRequested);
            Map(x => x.NewPasswordKey);
            Map(x => x.NewPasswordRequested);
            Map(x => x.Password);
            Map(x => x.PasswordSalt);
        }
    }
}