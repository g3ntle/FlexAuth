using System;

namespace FlexAuth.Security
{
    public interface IUser
    {
        #region Methods

        void Login();
        ICredentials GetCredentials();

        #endregion
    }
}
