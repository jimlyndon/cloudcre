using NHibernate;

namespace Cloudcre.Repository.NHibernate
{
    public interface INHibernateSessionFactoryBuilder
    {
        ISessionFactory GetSessionFactory();
    }
}
