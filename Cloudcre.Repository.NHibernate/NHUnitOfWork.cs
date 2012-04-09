using System;
using Cloudcre.Model.Core;
using Cloudcre.Model.Core.UnitOfWork;
using NHibernate;

namespace Cloudcre.Repository.NHibernate
{
    public class NHUnitOfWork : IUnitOfWork
    {
        ISession _session;

        public NHUnitOfWork(ISession session)
        {
            _session = session;
        }

        #region IUnitOfWork Members

        public void RegisterAmended(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _session.SaveOrUpdate(entity);
        }

        public void RegisterNew(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _session.Save(entity);
        }

        public void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _session.Delete(entity);
        }

        public void Commit()
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion
    }
}