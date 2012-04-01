using System;
using Cloudcre.Model.Core.Querying;
using System.Linq.Expressions;

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