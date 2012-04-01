using System.Collections.Generic;
using NHibernate.Search.Attributes;

namespace Cloudcre.Model.Core
{
    public abstract class EntityBase<T>
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        [DocumentId]
        public virtual T Id { get; set; }

        protected abstract void Validate();

        public virtual IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected virtual void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public override bool Equals(object entity)
        {
            return entity != null
                && entity is EntityBase<T>
                && this == (EntityBase<T>)entity;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<T> entity1, EntityBase<T> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }
            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }
            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase<T> entity1, EntityBase<T> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}