using System;

namespace FlexAuth.Security
{
    public class UserBase : IUser
    {
        #region Fields

        private bool isSignedIn = false;

        #endregion


        #region Events

        public event EventHandler SignedIn;
        public event EventHandler SignedOut;

        #endregion


        #region Properties

        public ICredentials Credentials { get; set; }
        public INodeRepository Nodes { get; set; }
        public object MetaData { get; set; }

        #endregion


        #region Methods

        public void SignIn()
        {
            try
            {
                UserManager.Instance.SignIn(this);
                OnSignedIn();
            }
            catch(AuthException e)
            {
                throw new SignInException("Cannot sign in", e.ErrorCode, e);
            }
        }

        public void SignOut()
        {
            try
            {
                UserManager.Instance.SignOut(this);
                OnSignedOut();
            }
            catch(AuthException e)
            {
                throw new SignOutException("Unable to sign out", e.ErrorCode, e);
            }
        }

        public bool IsSignedIn()
        {
            return isSignedIn;
        }

        public bool HasPermission(string node)
        {
            return Nodes?.HasNode(node) ?? false;
        }

        public T Convert<T>() where T : class
        {
            return (MetaData != null && MetaData is T ? (T)MetaData : null);
        }

        protected void OnSignedIn()
        {
            isSignedIn = true;
            SignedIn?.Invoke(this, EventArgs.Empty);
        }

        protected void OnSignedOut()
        {
            isSignedIn = false;
            SignedOut?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
