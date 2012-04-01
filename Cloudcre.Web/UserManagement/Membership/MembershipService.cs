using System;
using System.Web.Security;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Model.Specifications;
using Cloudcre.Service.Messages;

namespace Cloudcre.Web.UserManagement.Membership
{
    public class MembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public MembershipService(MembershipProvider provider, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            // membership provider, if not injected is configurable in web.config
            _provider = provider ?? System.Web.Security.Membership.Provider;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public int MinPasswordLength
        {
            get { return _provider.MinRequiredPasswordLength; }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public string GetCanonicalUsername(string userName)
        {
            var user = _provider.GetUser(userName, true);
            return user != null ? user.UserName : null;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser != null && currentUser.ChangePassword(oldPassword, newPassword);
        }

        public ActivateUserResponse ActivateUser(ActivateUserRequest request)
        {
            var response = new ActivateUserResponse();

            try
            {
                var user = _userRepository.FindFirstOrDefault(new UserByNameSpecification(request.UserName));

                if (user != null)
                {
                    if (user.NewEmailKey == request.Key)
                    {
                        user.IsActivated = true;
                        user.LastModifiedDate = DateTime.Now;
                        user.NewEmailKey = null;

                        _userRepository.Save(user);
                        _unitOfWork.Commit();
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Incorrect user activation key.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Cannot activate non existent user.";
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem activating this user.";
                // TODO: log exception
            }

            return response;
        }
    }
}