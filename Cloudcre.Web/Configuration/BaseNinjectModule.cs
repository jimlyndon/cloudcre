using System.Configuration;
using System.Web.Security;
using NHibernate;
using NHibernate.Search;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Cloudcre.Infrastructure.Configuration;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Infrastructure.Logging;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Repository.NHibernate;
using Cloudcre.Repository.NHibernate.Repositories;
using Cloudcre.Web.UserManagement.Membership;

namespace Cloudcre.Web.Configuration
{
    internal class BaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Configure NHibernate session factory implementation
            Bind<INHibernateSessionFactoryBuilder>()
                .To<NHibernateSessionFactoryBuilder>().InSingletonScope()
                .WithConstructorArgument("connectionString",
                    ConfigurationManager.ConnectionStrings["Cloudcre.Repository.WhitneySalesConnectionString"].ConnectionString)
                .WithConstructorArgument("luceneIndex",
                    ConfigurationManager.AppSettings["LuceneIndex"]);

            // Create Nhibernate session implementation from session factory and bind it to interface
            Bind<ISession>()
                .ToMethod(ctx => ctx.Kernel.Get<INHibernateSessionFactoryBuilder>()
                .GetSessionFactory().OpenSession())
                .InRequestScope();

            // NHibernate full text search session
            Bind<IFullTextSession>()
                .ToMethod(ctx => Search.CreateFullTextSession(ctx.Kernel.Get<ISession>())).InRequestScope();

            // Access to config files
            Bind<IConfigurationManager>().To<WebConfigurationManagerAdapter>().InSingletonScope();

            // logging
            Bind<ILogger>().To<Log4NetAdapter>().InSingletonScope();

            // cookie management
            Bind<ICookieStorageService>().To<CookieStorageService>();

            // business repositories
            Bind<IUnitOfWork>().To<NHUnitOfWork>().InRequestScope();
            Bind<IPropertyRepository>().To<PropertyRepository>();
            Bind<IMultipleFamilyRepository>().To<MultipleFamilyRepository>();
            Bind<IOfficeRepository>().To<OfficeRepository>();
            Bind<IRetailRepository>().To<RetailRepository>();
            Bind<IIndustrialRepository>().To<IndustrialRepository>();
            Bind<IIndustrialCondominiumRepository>().To<IndustrialCondominiumRepository>();
            Bind<ICommercialCondominiumRepository>().To<CommercialCondominiumRepository>();
            Bind<ICommercialLandRepository>().To<CommercialLandRepository>();
            Bind<IResidentialLandRepository>().To<ResidentialLandRepository>();
            Bind<IIndustrialLandRepository>().To<IndustrialLandRepository>();

            // user management
            Bind<IUserRepository>().To<UserRepository>().InRequestScope()
                .WithConstructorArgument("session", Search.CreateFullTextSession(Kernel.Get<ISession>()));
            Bind<MembershipProvider>().ToMethod(ctx => Membership.Provider).InRequestScope();
            Bind<IMembershipService>().To<MembershipService>();
            Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
        }
    }
}