using System;

namespace FlexAuth.Security
{
    public class UserManager
    {
        #region Fields

        private static UserManager instance;

        #endregion

        #region Constructors

        private UserManager() { }

        #endregion


        #region Events

        public delegate void UserSignedInHandler(object sender, UserEventArgs e);
        public event UserSignedInHandler UserSignedIn;

        public delegate void UserSignedOutHandler(object sender, UserEventArgs e);
        public event UserSignedOutHandler UserSignedOut;

        #endregion


        #region Properties

        private IUser _current;
        public IUser Current
        {
            get { return _current; }
            internal set
            {
                var previous = _current;
                _current = value;

                if (previous == null && _current != null)
                    OnUserSignedIn(_current);
                else if (previous != null && _current == null)
                    OnUserSignedOut(previous);
                else if (previous != null && _current != null)
                {
                    OnUserSignedOut(previous);
                    OnUserSignedIn(_current);
                }

                _current = value;
            }
        }

        public bool HasUser
        {
            get { return Current != null; }
        }

        #endregion


        #region Methods

        protected void OnUserSignedIn(IUser user)
        {
            UserSignedIn?.Invoke(this, new UserEventArgs(user));
        }

        protected void OnUserSignedOut(IUser user)
        {
            UserSignedOut?.Invoke(this, new UserEventArgs(user));
        }

        public void SignIn(IUser user)
        {
            if (user == null)
                throw new SecurityException("User cannot be null");
            else if (user.IsSignedIn())
                throw new SecurityException("User already signed in");
            else if (user.Credentials == null)
                throw new SecurityException("Credentials cannot be null");
            else if (!user.Credentials.Check())
                throw new SecurityException("Invalid credentials");

            Current = user;
        }

        public void SignOut(IUser user)
        {
            if (user == null)
                throw new SecurityException("User cannot be null");
            else if (!user.Equals(Current))
                throw new SecurityException("User must be signed in");

            Current = null;
        }

        public bool HasPermission(string node)
        {
            return Current?.HasPermission(node) ?? false;
        }

        public static UserManager GetInstance()
        {
            return instance ?? (instance = new UserManager());
        }

        #endregion
    }
}
