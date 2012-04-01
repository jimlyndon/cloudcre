using System;
using System.Collections.Generic;
using Cloudcre.Model.Core;
using Cloudcre.Model.Core.Querying;

namespace Cloudcre.Model
{
    public interface IUserRepository : IRepository<User, Guid> { }
    public interface IBuildingPropertyRepository<T, TId> : IRepository<T, TId> 
        where T : BuildingProperty { }
    public interface IPropertyRepository : IRepository<Property, Guid> { }
    public interface IMultipleFamilyRepository : IBuildingPropertyRepository<MultipleFamily, Guid> { }
    public interface IOfficeRepository : IBuildingPropertyRepository<Office, Guid> { }
    public interface IRetailRepository : IBuildingPropertyRepository<Retail, Guid> { }
    public interface IIndustrialRepository : IBuildingPropertyRepository<Industrial, Guid> { }
    public interface IIndustrialCondominiumRepository : IBuildingPropertyRepository<IndustrialCondominium, Guid> { }
    public interface ICommercialCondominiumRepository : IBuildingPropertyRepository<CommercialCondominium, Guid> { }
    public interface ICommercialLandRepository : IBuildingPropertyRepository<CommercialLand, Guid> { }
    public interface IResidentialLandRepository : IRepository<ResidentialLand, Guid> { }
    public interface IIndustrialLandRepository : IBuildingPropertyRepository<IndustrialLand, Guid> { }

    public interface IRepository<T, in TId> 
        where T : IAggregateRoot
    {
        void Add(T domainObject);
        void Remove(T domainObject);
        void Save(T domainObject);
        T FindBy(TId id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(int index, int count);
        T FindFirst(Specification<T> specification);
        T FindFirstOrDefault(Specification<T> specification);
        IEnumerable<T> FindAll(Specification<T> specification);
        IEnumerable<T> FindAll(Specification<T> specification, int index, int count);
        IEnumerable<T> FindBy(SeachQuery[] queries);
        IEnumerable<T> FindBy(SeachQuery[] queries, int index, int count);
        IEnumerable<T> FindBy(Specification<T> specification, SeachQuery[] queries);
        IEnumerable<T> FindBy(Specification<T> specification, SeachQuery[] queries, int index, int count);
    }
}