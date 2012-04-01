using System;
using System.Globalization;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using Cloudcre.Service.Messages;
using Cloudcre.Web.Filters;
using Cloudcre.Web.Models;
using Cloudcre.Web.UserManagement.Membership;

namespace Cloudcre.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IMembershipService _membershipService;

        public AccountController(IFormsAuthenticationService formsAuthenticationService, IMembershipService membershipService)
        {
            _formsAuthenticationService = formsAuthenticationService;
            _membershipService = membershipService;
        }

        //
        // GET: /Account/LogOn

        [AllowAnonymousAccess]
        public ActionResult LogOn()
        {
            return View();
        }
        
        //
        // POST: /Account/LogOn
        
        [HttpPost]
        [AllowAnonymousAccess]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (!ValidateLogOn(model.UserName, model.Password))
            {
                return View();
            }

            _formsAuthenticationService.SignIn(model.UserName, model.RememberMe);

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Search");
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            _formsAuthenticationService.SignOut();

            return RedirectToAction("Index", "Search");
        }


        ////Register and Activate turned off
        //
        // GET: /Account/Register
        //[AllowAnonymousAccess]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //
        // POST: /Account/Register

        //[HttpPost]
        //[AllowAnonymousAccess]
        //public ActionResult Register(string userName, string email, string password, string confirmPassword)
        //{
        //    // username is email
        //    string userNameForEmail = userName;

        //    if (ValidateRegistration(userName, userNameForEmail, password, confirmPassword))
        //    {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus = _membershipService.CreateUser(userName, password, userNameForEmail);

        //        if (createStatus == MembershipCreateStatus.Success)
        //        {
        //            _formsAuthenticationService.SignIn(userName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Property");
        //        }

        //        ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View();
        //}

        //
        // URL: /Account/Activate/username/key

        //public ActionResult Activate(string username, string key)
        //{
        //    var request = new ActivateUserRequest
        //    {
        //        Key = key,
        //        UserName = username
        //    };

        //    _membershipService.ActivateUser(request);

        //    //if (response.Success == false)
        //    //    return RedirectToAction("Index", "Property");

        //    // Success or fail, redirect to logon screen
        //    return RedirectToAction("LogOn");
        //}

        //
        // GET: /Account/ChangePassword

        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ChangePassword

        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    ViewData["PasswordLength"] = _membershipService.MinPasswordLength;

        //    if (!ValidateChangePassword(model.OldPassword, model.NewPassword, model.ConfirmPassword))
        //    {
        //        return View();
        //    }

        //    try
        //    {
        //        if (_membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
        //            return View();
        //        }
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
        //        return View();
        //    }
        //}

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Status Codes

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }

            if (newPassword == null || newPassword.Length < _membershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         _membershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }

            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }

            if (!_membershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }

            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }

            if (password == null || password.Length < _membershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         _membershipService.MinPasswordLength));
            }

            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }
        
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        
        #endregion
    }
}
