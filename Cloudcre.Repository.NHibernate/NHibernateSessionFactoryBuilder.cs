using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Lucene.Net.Analysis.Standard;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Search.Event;
using NHibernate.Search.Store;
using NH = NHibernate;

namespace Cloudcre.Repository.NHibernate
{
    public class NHibernateSessionFactoryBuilder : INHibernateSessionFactoryBuilder
    {
        private string _connectionString;
        private string _luceneIndex;

        public NHibernateSessionFactoryBuilder(string connectionString, string luceneIndex)
        {
            _connectionString = connectionString;
            _luceneIndex = luceneIndex;
        }

        private ISessionFactory _sessionFactory;

        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = CreateSessionFactory();
            }

            return _sessionFactory;
        }

        private ISessionFactory CreateSessionFactory()
        {
            var config = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.Dialect<MySQLDialect>().ShowSql()
                .ConnectionString(_connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionFactoryBuilder>())
                .ExposeConfiguration (
                cfg =>
                {
                    cfg.SetProperty("hibernate.search.default.directory_provider", typeof(FSDirectoryProvider).AssemblyQualifiedName);
                    cfg.SetProperty("hibernate.search.default.indexBase", _luceneIndex);
                    cfg.SetProperty("hibernate.search.default.indexBase.create", "true");
                    cfg.SetProperty("hibernate.search.analyzer", typeof(StandardAnalyzer).AssemblyQualifiedName);

                    cfg.SetListener(NH.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());
                    cfg.SetListener(NH.Event.ListenerType.PostInsert, new FullTextIndexEventListener());
                    cfg.SetListener(NH.Event.ListenerType.PostDelete, new FullTextIndexEventListener());
                }
            ).BuildConfiguration();

            //config.SetProperty("hibernate.search.default.directory_provider", typeof(FSDirectoryProvider).AssemblyQualifiedName);
            //config.SetProperty("hibernate.search.default.indexBase", "~/LuceneIndex");
            //config.SetProperty("hibernate.search.default.indexBase.create", "true");

            //config.SetListener(NH.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());
            //config.SetListener(NH.Event.ListenerType.PostInsert, new FullTextIndexEventListener());
            //config.SetListener(NH.Event.ListenerType.PostDelete, new FullTextIndexEventListener());

            return config.BuildSessionFactory();

            //return Fluently.Configure()
            //    .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromAppSetting("DBConnString")))
            //    .Mappings(m => m.AutoMappings.Add(AutoPersistenceModel.MapEntitiesFromAssemblyOf<DomainClass>()
            //                                    .Where(w => w.BaseType == typeof(DomainEntity))
            //                                    .ConventionDiscovery.Add<PrimaryKeyConvention>()
            //                                    .WithSetup(convention =>
            //                                    {
            //                                        convention.FindIdentity = t => t.Name == "Id";
            //                                        convention.IsBaseType = type => type == typeof(DomainEntity);
            //                                    }
            //                                    )))
            //    .BuildSessionFactory();
        }
    }
}
