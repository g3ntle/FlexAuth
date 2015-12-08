using System;

namespace FlexAuth.Security
{
    public interface IUser
    {
        #region Events

        event EventHandler SignedIn;
        event EventHandler SignedOut;

        #endregion


        #region Properties

        ICredentials Credentials { get; set; }
        INodeRepository Nodes { get; set; }
        object MetaData { get; set; }

        #endregion


        #region Methods

        void SignIn();
        bool IsSignedIn();
        bool HasPermission(string node);
        T Convert<T>() where T : class;

        #endregion
    }
}
