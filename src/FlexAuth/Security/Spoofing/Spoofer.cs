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
            var str = ConfigurationManager.AppSettings["Spoofing"]?.ToString();
            if (String.IsNullOrEmpty(str))
                return;

            var args = str.GetValues();
            if (args == null)
                throw new SpoofException(new NullReferenceException("Args can\'t be null"));

            try
            {
                _args = Bean.Populate<SpoofArgs>(args);
                Spoof();
            }
            catch (Exception ex)
            {
                throw new SpoofException(ex);
            }
        }

        private void Spoof()
        {
            if (_args == null)
                new NullReferenceException("Args can\'t be null");

            var user = CreateUser(_args.UserType);
            var cred = CreateCredentials(_args.CredentialsType);

            var str = ConfigurationManager.AppSettings["Credentials"]?.ToString();
            if (String.IsNullOrEmpty(str))
                throw new NullReferenceException("No credentials supplied");

            var args = str.GetValues();
            if (args == null)
                throw new NullReferenceException("Args can\'t be null");

            cred.Populate(args);

            _user = user;
            _user.Credentials = cred;
        }

        private IUser CreateUser(string typeName)
        {
            var asm = Assembly.GetEntryAssembly();
            var type = asm.GetTypeByName(typeName);

            if (type == null)
                throw new NullReferenceException($"Type {typeName} doesn\'t exist");
            else if (!type.IsSubclassOf(typeof(IUser)))
                new InvalidCastException($"Type {type} doesn\'t implement IUser");

            return Activator.CreateInstance(type) as IUser;
        }

        private ICredentials CreateCredentials(string typeName)
        {
            var asm = Assembly.GetEntryAssembly();
            var type = asm.GetTypeByName(typeName);

            if (type == null)
                throw new NullReferenceException($"Type {typeName} doesn\'t exist");
            else if (!type.IsSubclassOf(typeof(ICredentials)))
                new InvalidCastException($"Type {type} doesn\'t implement ICredentials");

            return Activator.CreateInstance(type) as ICredentials;
        }

        #endregion
    }
}
