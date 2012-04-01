using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web.Security;
using Ninject;
using Cloudcre.Model;
using Cloudcre.Model.Core.UnitOfWork;
using Cloudcre.Model.Specifications;
using Cloudcre.Web.App_Start;

namespace Cloudcre.Web.UserManagement.Membership.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval = false;
        private bool _requiresQuestionAndAnswer = false;
        private bool _requiresUniqueEmail = true;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredPasswordLength;
        private int _minRequiredNonalphanumericCharacters;
        private string _passwordStrengthRegularExpression;
        private MembershipPasswordFormat _passwordFormat = MembershipPasswordFormat.Hashed;

        [Inject]
        public IUserRepository Repository { get; set; }

        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public CustomMembershipProvider()
        {
            NinjectWebCommon.Kernel.Inject(this);
        }

        public MembershipUser CreateUser(string username, string password, string email)
        {
            var user = new User
                           {
                               Name = username,
                               Email = email,
                               PasswordSalt = CreateSalt(),
                               CreatedDate = DateTime.Now,
                               IsActivated = false,
                               IsLockedOut = false,
                               LastLockedOutDate = DateTime.Now,
                               LastLoginDate = DateTime.Now,
                               NewEmailKey = GenerateEmailKey()
                           };

            user.Password = CreatePasswordHash(password, user.PasswordSalt);

            Repository.Save(user);
            UnitOfWork.Commit();

            return GetUser(username);
        }

        public override string GetUserNameByEmail(string email)
        {
            var user = Repository.FindFirstOrDefault(new UserByEmailSpecification(email));

            return user != null ? user.Name : null;
        }

        public MembershipUser GetUser(string username)
        {
            var user = Repository.FindFirstOrDefault(new UserByNameSpecification(username));

            if (user != null)
            {
                string userName = user.Name;
                Guid providerUserKey = user.Id;
                string email = user.Email;
                string passwordQuestion = "";
                string comment = user.Comments;
                bool isApproved = user.IsActivated;
                bool isLockedOut = user.IsLockedOut;
                DateTime creationDate = user.CreatedDate;
                DateTime lastLoginDate = user.LastLoginDate;
                DateTime lastActivityDate = DateTime.Now;
                DateTime lastPasswordChangedDate = DateTime.Now;
                DateTime lastLockedOutDate = user.LastLockedOutDate;

                var newUser = new MembershipUser("CustomMembershipProvider",
                                                 userName,
                                                 providerUserKey,
                                                 email,
                                                 passwordQuestion,
                                                 comment,
                                                 isApproved,
                                                 isLockedOut,
                                                 creationDate,
                                                 lastLoginDate,
                                                 lastActivityDate,
                                                 lastPasswordChangedDate,
                                                 lastLockedOutDate);

                return newUser;
            }
            return null;
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = Repository.FindFirstOrDefault(new UserByNameSpecification(username));

            if (user != null)
            {
                if (user.Password == CreatePasswordHash(password, user.PasswordSalt) && user.IsActivated)
                    return true;
            }
            return false;
        }

        private static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        private static string GenerateEmailKey()
        {
            Guid emailKey = Guid.NewGuid();

            return emailKey.ToString();
        }

        //
        // A helper function to retrieve config values from the configuration file.
        //
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "CustomMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Membership Provider");
            }

            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"],
                                              System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts = Convert.ToInt32(
                GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow = Convert.ToInt32(
                GetConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonalphanumericCharacters = Convert.ToInt32(
                GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(
                GetConfigValue(config["minRequiredPasswordLength"], "6"));
            _enablePasswordReset = Convert.ToBoolean(
                GetConfigValue(config["enablePasswordReset"], "true"));
            _passwordStrengthRegularExpression = Convert.ToString(
                GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string username,
                                                  string password,
                                                  string email,
                                                  string passwordQuestion,
                                                  string passwordAnswer,
                                                  bool isApproved,
                                                  object providerUserKey,
                                                  out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                CreateUser(username, password, email);
                status = MembershipCreateStatus.Success;

                return GetUser(username, false);
            }

            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return GetUser(username);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}