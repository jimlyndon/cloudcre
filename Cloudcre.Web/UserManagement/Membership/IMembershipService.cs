using System.Web.Security;
using Cloudcre.Service.Messages;

namespace Cloudcre.Web.UserManagement.Membership
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        bool ValidateUser(string userName, string password);
        string GetCanonicalUsername(string userName);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        ActivateUserResponse ActivateUser(ActivateUserRequest request);
    }
}