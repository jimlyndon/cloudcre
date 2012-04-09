using Cloudcre.Service.Messages;

namespace Cloudcre.Service.Property.Messages
{
    public class GetPropertyResponse<TVm> : ResponseBase
    {
        public TVm ViewModel { get; set; }
    }
}