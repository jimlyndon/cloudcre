using System;
using System.Linq.Expressions;
using Cloudcre.Model.Core.Querying;

namespace Cloudcre.Model.Specifications
{
    public class UserByNameSpecification : Specification<User>
    {
        private readonly string _name;

        public UserByNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<User, bool>> IsSatisfied()
        {
            Expression<Func<User, bool>> result = p => p.Name == _name;
            return result;
        }
    }
}