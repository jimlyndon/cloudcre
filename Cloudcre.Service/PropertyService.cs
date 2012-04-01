using System;
using System.Collections.Generic;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Service.Messages;
using Cloudcre.Service.ViewModels;

namespace Cloudcre.Service
{
    public interface IBuildingPropertyService<T, TId, TVm> : IPropertyBaseService<T, TId, TVm>
        where T : BuildingProperty
        where TVm : BuildingPropertyViewModel { }
    public interface IPropertyService : IPropertyBaseService<Property, Guid, PropertyViewModel> { }
    public interface IMultipleFamilyService : IBuildingPropertyService<MultipleFamily, Guid, MultipleFamilyViewModel> { }
    public interface IOfficeService : IBuildingPropertyService<Office, Guid, OfficeViewModel> { }
    public interface IRetailService : IBuildingPropertyService<Retail, Guid, RetailViewModel> { }
    public interface IIndustrialService : IBuildingPropertyService<Industrial, Guid, IndustrialViewModel> { }
    public interface IIndustrialCondominiumService : IBuildingPropertyService<IndustrialCondominium, Guid, IndustrialCondominiumViewModel> { }
    public interface ICommercialCondominiumService : IBuildingPropertyService<CommercialCondominium, Guid, CommercialCondominiumViewModel> { }
    public interface ICommercialLandService : IBuildingPropertyService<CommercialLand, Guid, CommercialLandViewModel> { }
    public interface IResidentialLandService : IPropertyBaseService<ResidentialLand, Guid, ResidentialLandViewModel> { }
    public interface IIndustrialLandService : IBuildingPropertyService<IndustrialLand, Guid, IndustrialLandViewModel> { }

    public abstract class BuildingPropertyService<T, TId, TVm> : PropertyBaseService<T, TId, TVm>,
                                                                 IBuildingPropertyService<T, TId, TVm>
        where T : BuildingProperty
        where TVm : BuildingPropertyViewModel
    {
        protected BuildingPropertyService(IBuildingPropertyRepository<T, TId> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public override SearchPropertyResponse<TVm> SearchProperties(SearchPropertyRequest request)
        {
            return SearchProperties(request, FilterPropertiesByTotalSqFtRange);
        }

        private IEnumerable<T> FilterPropertiesByTotalSqFtRange(IEnumerable<T> productsMatchingRefinement,
                                                                SearchPropertyRequest request)
        {
            if (request.SqftMinFilter.HasValue && request.SqftMaxFilter.HasValue)
            {
                return
                    productsMatchingRefinement.Between(
                        x => x.BuildingTotalSquareFoot != null ? x.BuildingTotalSquareFoot.Value : 0,
                        request.SqftMinFilter.Value, request.SqftMaxFilter.Value);
            }

            return productsMatchingRefinement;
        }
    }
    
    public class PropertyService : PropertyBaseService<Property, Guid, PropertyViewModel>, IPropertyService
    {
        public PropertyService(IPropertyRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class MultipleFamilyService : BuildingPropertyService<MultipleFamily, Guid, MultipleFamilyViewModel>, IMultipleFamilyService
    {
        public MultipleFamilyService(IMultipleFamilyRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class OfficeService : BuildingPropertyService<Office, Guid, OfficeViewModel>, IOfficeService
    {
        public OfficeService(IOfficeRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class RetailService : BuildingPropertyService<Retail, Guid, RetailViewModel>, IRetailService
    {
        public RetailService(IRetailRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class IndustrialService : BuildingPropertyService<Industrial, Guid, IndustrialViewModel>, IIndustrialService
    {
        public IndustrialService(IIndustrialRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class IndustrialCondominiumService : BuildingPropertyService<IndustrialCondominium, Guid, IndustrialCondominiumViewModel>, IIndustrialCondominiumService
    {
        public IndustrialCondominiumService(IIndustrialCondominiumRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class CommercialCondominiumService : BuildingPropertyService<CommercialCondominium, Guid, CommercialCondominiumViewModel>, ICommercialCondominiumService
    {
        public CommercialCondominiumService(ICommercialCondominiumRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class CommercialLandService : BuildingPropertyService<CommercialLand, Guid, CommercialLandViewModel>, ICommercialLandService
    {
        public CommercialLandService(ICommercialLandRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class ResidentialLandService : PropertyBaseService<ResidentialLand, Guid, ResidentialLandViewModel>, IResidentialLandService
    {
        public ResidentialLandService(IResidentialLandRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class IndustrialLandService : BuildingPropertyService<IndustrialLand, Guid, IndustrialLandViewModel>, IIndustrialLandService
    {
        public IndustrialLandService(IIndustrialLandRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}