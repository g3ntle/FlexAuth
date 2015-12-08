using FlexAuth.Security.Spoofing;
using System;

namespace FlexAuth.Security
{
    public class UserManager
    {
        #region Fields

        private static UserManager instance;

        private Spoofer spoofer;
        private bool isSpoofed;

        #endregion

        #region Constructors

        private UserManager()
        {
            Initialize();
        }

        #endregion


        #region Events

        public delegate void UserSignedInHandler(object sender, UserEventArgs e);
        public event UserSignedInHandler UserSignedIn;

        public delegate void UserSignedOutHandler(object sender, UserEventArgs e);
        public event UserSignedOutHandler UserSignedOut;

        #endregion


        #region Properties

        public Spoofer Spoofer
        {
            get { return spoofer; }
        }

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

        public static UserManager Instance
        {
            get
            {
                return instance ?? (instance = new UserManager());
            }
        }

        #endregion


        #region Methods

        private void Initialize()
        {
            spoofer = new Spoofer(this);
            isSpoofed = (spoofer.User != null);
        }

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
                throw new SignInException("User cannot be null", 100);
            else if (Current?.IsSignedIn() ?? false)
                throw new SignInException("User already signed in", 101);
            else if (user.Credentials == null)
                throw new SignInException("Credentials cannot be null", 102);
            else if (!user.Credentials.Check())
                throw new SignInException("Invalid credentials", 103);

            user.MetaData = user.Credentials.MetaData;
            Current = user;
        }

        public void SignOut(IUser user)
        {
            if (user == null)
                throw new SignOutException("User cannot be null", 200);
            else if (!user.Equals(Current))
                throw new SignOutException("User must be signed in", 201);

            Current = null;
        }

        public T GetUserMetaDataAs<T>() where T : class
        {
            return Current?.Convert<T>();
        }

        public bool HasPermission(string node)
        {
            return Current?.HasPermission(node) ?? false;
        }

        public void CleanUp()
        {
            Current = null;
        }

        #endregion
    }
}
