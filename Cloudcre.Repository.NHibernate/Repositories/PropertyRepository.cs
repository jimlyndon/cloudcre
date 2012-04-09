using System;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using NHibernate.Search;

namespace Cloudcre.Repository.NHibernate.Repositories
{
    public class PropertyRepository : Repository<Property, Guid>, IPropertyRepository
    {
        public PropertyRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class MultipleFamilyRepository : Repository<MultipleFamily, Guid>, IMultipleFamilyRepository
    {
        public MultipleFamilyRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class OfficeRepository : Repository<Office, Guid>, IOfficeRepository
    {
        public OfficeRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class RetailRepository : Repository<Retail, Guid>, IRetailRepository
    {
        public RetailRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class IndustrialRepository : Repository<Industrial, Guid>, IIndustrialRepository
    {
        public IndustrialRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class IndustrialCondominiumRepository : Repository<IndustrialCondominium, Guid>, IIndustrialCondominiumRepository
    {
        public IndustrialCondominiumRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class CommercialCondominiumRepository : Repository<CommercialCondominium, Guid>, ICommercialCondominiumRepository
    {
        public CommercialCondominiumRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class CommercialLandRepository : Repository<CommercialLand, Guid>, ICommercialLandRepository
    {
        public CommercialLandRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class ResidentialLandRepository : Repository<ResidentialLand, Guid>, IResidentialLandRepository
    {
        public ResidentialLandRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }

    public class IndustrialLandRepository : Repository<IndustrialLand, Guid>, IIndustrialLandRepository
    {
        public IndustrialLandRepository(IUnitOfWork unitOfWork, IFullTextSession session)
            : base(unitOfWork, session)
        {
        }
    }
}