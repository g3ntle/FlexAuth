using System;

namespace FlexAuth.Security
{
    public interface ICredentials
    {
        #region Properties

        object MetaData { get; set; }

        #endregion


        #region Methods

        bool Check();

        #endregion
    }
}
