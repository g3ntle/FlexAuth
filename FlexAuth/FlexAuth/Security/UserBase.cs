using System;

namespace FlexAuth.Security
{
    public class UserBase : IUser
    {
        #region Fields

        private ICredentials credentials;

        #endregion


        #region Constructors

        public UserBase(ICredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            this.credentials = credentials;
        }

        #endregion


        #region Methods

        public virtual void Login()
        {
            UserManager.GetInstance().Login(this);
        }

        public ICredentials GetCredentials()
        {
            return credentials;
        }

        #endregion
    }
}
