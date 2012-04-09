using System;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using NHibernate.Search;

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