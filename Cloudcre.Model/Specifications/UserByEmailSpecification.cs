using System;
using Cloudcre.Model.Core.Querying;
using System.Linq.Expressions;

namespace Cloudcre.Model.Specifications
{
    public class UserByEmailSpecification : Specification<User>
    {
        private readonly string _email;

        public UserByEmailSpecification(string email)
        {
            _email = email;
        }

        public override Expression<Func<User, bool>> IsSatisfied()
        {
            Expression<Func<User, bool>> result = p => p.Email == _email;
            return result;
        }
    }
}