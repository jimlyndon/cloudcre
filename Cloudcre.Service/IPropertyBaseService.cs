using Cloudcre.Model.Core;
using Cloudcre.Service.Messages;

namespace Cloudcre.Service
{
    public interface IPropertyBaseService<T, TId, TVm> where T : IAggregateRoot
    {
        SearchPropertyResponse<TVm> SearchProperties(SearchPropertyRequest request);
        
        AddPropertyResponse AddProperty(AddPropertyRequest<TVm> request);

        RemovePropertyResponse RemoveProperty(RemovePropertyRequest<TId> request);
    }
}