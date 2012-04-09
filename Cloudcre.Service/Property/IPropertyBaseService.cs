using Cloudcre.Model.Core;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Report.Messages;

namespace Cloudcre.Service.Property
{
    public interface IPropertyBaseService<T, TId, TVm> where T : IAggregateRoot
    {
        SearchPropertyResponse<TVm> SearchProperties(SearchPropertyRequest request);
        
        AddPropertyResponse AddProperty(AddPropertyRequest<TVm> request);

        RemovePropertyResponse RemoveProperty(RemovePropertyRequest<TId> request);

        GetReportResponse SummaryReport(GetReportRequest request);
    }
}