using System;

namespace FlexAuth.Security
{
    public interface ICredentials
    {
        #region Methods

        bool Check();

        #endregion
    }
}
