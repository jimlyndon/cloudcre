using System;
using System.Data;
using Cloudcre.Utilities.Console;
using NHibernate.Search;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Repository.NHibernate;
using Cloudcre.Repository.NHibernate.Repositories;

namespace Cloudcre.Test.DataLoader.Marshallers
{
    public class AccountMarshaller : CsvMarshaller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFullTextSession _session;

        public AccountMarshaller(EnvironmentContext.Type environmentType)
        {
            var sessionfactorybuilder = new NHibernateSessionFactoryBuilder(EnvironmentContext.ConnectionString(environmentType), "~/LuceneIndex");

            _session = Search.CreateFullTextSession(sessionfactorybuilder.GetSessionFactory().OpenSession());

            _unitOfWork = new NHUnitOfWork(_session);
        }

        protected override void Marshall(DataTable table)
        {
            IUserRepository userRepository = new UserRepository(_unitOfWork, _session);

            for (int count = 0; count < table.Rows.Count; count++)
            {
                var user = new User
                {
                    Name = table.GetRowValue<string>("Name", count),
                    Email = table.GetRowValue<string>("Email", count),
                    Password = table.GetRowValue<string>("Password", count),
                    PasswordSalt = table.GetRowValue<string>("PasswordSalt", count),
                    CreatedDate = DateTime.Now,
                    IsActivated = Convert.ToBoolean(table.Rows[count]["IsActivated"])
                };

                userRepository.Add(user);
            }

            _unitOfWork.Commit();
            _session.Dispose();
        }
    }
}