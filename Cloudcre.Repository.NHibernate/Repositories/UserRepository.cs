using System;
using NHibernate.Search;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;

namespace Cloudcre.Repository.NHibernate.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IFullTextSession session) 
            : base(unitOfWork, session)
        {
        }
    }
}