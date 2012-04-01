using System;
using Cloudcre.Model.Core;

namespace Cloudcre.Model
{
    public class User : EntityBase<Guid>, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual string Comments { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime? LastModifiedDate { get; set; }

        public virtual DateTime LastLoginDate { get; set; }

        public virtual string LastLoginIp { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual bool IsLockedOut { get; set; }

        public virtual DateTime LastLockedOutDate { get; set; }

        public virtual string LastLockedOutReason { get; set; }

        public virtual string NewPasswordKey { get; set; }

        public virtual DateTime? NewPasswordRequested { get; set; }

        public virtual string NewEmail { get; set; }

        public virtual string NewEmailKey { get; set; }

        public virtual DateTime? NewEmailRequested { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}