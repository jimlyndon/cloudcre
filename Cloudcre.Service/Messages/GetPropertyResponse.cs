namespace Cloudcre.Service.Messages
{
    public class GetPropertyResponse<TVm> : ResponseBase
    {
        public TVm ViewModel { get; set; }
    }
}