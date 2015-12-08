using Beans;
using FlexAuth.Utility;
using System;
using System.Configuration;
using System.Reflection;

namespace FlexAuth.Security.Spoofing
{
    public sealed class Spoofer
    {
        #region Fields

        private readonly UserManager parent;

        private string argString;
        private string credArgString;

        #endregion


        #region Constructors

        public Spoofer(UserManager parent)
        {
            this.parent = parent;
            Initialize();
        }

        #endregion


        #region Properties

        private SpoofArgs _args;
        public SpoofArgs Args
        {
            get { return _args; }
        }

        private IUser _user;
        public IUser User
        {
            get { return _user; }
        }

        #endregion


        #region Methods

        private void Initialize()
        {
            argString = ConfigurationManager.AppSettings["Spoofing"]?.ToString();
            if (String.IsNullOrEmpty(argString))
                return;

            try
            {
                Spoof();
            }
            catch (Exception ex)
            {
                throw new SpoofException(ex);
            }
        }

        private void Spoof()
        {
            // Parse arguments
            var args = argString.GetValues();
            if (args == null)
                throw new NullReferenceException("Args can\'t be null");

            // Populate arguments object
            _args = Bean.Populate<SpoofArgs>(args);

            // Get types from entry assembly
            var asm = Assembly.GetEntryAssembly();
            var userType = asm.GetType(_args?.UserType);
            var credType = asm.GetType(_args?.CredentialsType);

            // Check userType for null and inheritance
            if (userType == null)
                throw new NullReferenceException($"Invalid user type {_args.UserType ?? "n/a"}");
            else if(!typeof(IUser).IsAssignableFrom(userType))
                throw new InvalidCastException($"Type {userType} doesn\'t implement IUser");

            // Check credType for null and inheritance
            if (credType == null)
                throw new NullReferenceException($"Invalid credentials type {_args.CredentialsType ?? "n/a"}");
            else if (!typeof(ICredentials).IsAssignableFrom(credType))
                throw new InvalidCastException($"Type {credType} doesn\'t implement ICredentials");

            // Create runtime objects from gathered types
            var user = Activator.CreateInstance(userType) as IUser;
            var cred = Activator.CreateInstance(credType) as ICredentials;

            // Get credential arguments string
            credArgString = ConfigurationManager.AppSettings["Credentials"]?.ToString();

            // Parse credential arguments
            var credArgs = credArgString?.GetValues();
            if (args == null)
                throw new NullReferenceException("Args can\'t be null");

            // Populate credentials
            cred.Populate(credArgs, credType);

            // Asign
            _user = user;
            _user.Credentials = cred;
        }

        public void SignIn()
        {
            User?.SignIn();
        }

        #endregion
    }
}
