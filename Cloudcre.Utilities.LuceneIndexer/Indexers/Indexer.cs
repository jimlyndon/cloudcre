using System;
using System.ComponentModel;
using System.Linq;
using Cloudcre.Utilities.Console;
using NHibernate.Search;
using NHibernate.Search.Attributes;
using Cloudcre.Model.Core;
using Cloudcre.Repository.NHibernate;

namespace Cloudcre.Utilities.LuceneIndexer.Indexers
{
    public class Indexer
    {
        private readonly IFullTextSession _session;

        public Indexer(EnvironmentContext.Type environmentType)
        {
            var sessionfactorybuilder = new NHibernateSessionFactoryBuilder(
                EnvironmentContext.ConnectionString(environmentType), 
                EnvironmentContext.LuceneIndexRootDirectory(environmentType));

            _session = Search.CreateFullTextSession(sessionfactorybuilder.GetSessionFactory().OpenSession());
        }

        public void Run()
        {
            var entityTypes = typeof(EntityBase<>).Assembly.GetTypes()
                .Where(x => typeof(IAggregateRoot).IsAssignableFrom(x));

            foreach (var t in entityTypes.Where(t => TypeDescriptor.GetAttributes(t)[typeof(IndexedAttribute)] != null))
                ReindexEntity(t);

            _session.Dispose();
        }
        
        private void ReindexEntity(Type t)
        {
            var stop = false;
            var index = 0;
            const int pageSize = 500;

            do
            {
                var list = _session.CreateCriteria(t)
                    .SetFirstResult(index)
                    .SetMaxResults(pageSize).List();

                _session.Transaction.Begin();

                foreach (var itm in list)
                    _session.Index(itm);

                _session.Transaction.Commit();

                index += pageSize;

                if (list.Count < pageSize) 
                    stop = true;

            } while (!stop);
        }
    }
}