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

        public delegate void UserActionHandler(object sender, UserActionEventArgs e);
        public event UserActionHandler UserAction;

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
                    OnUserAction(_current, Action.Login);
                else if (previous != null && _current == null)
                    OnUserAction(previous, Action.Logout);
                else if (previous != null && _current != null)
                {
                    OnUserAction(previous, Action.Logout);
                    OnUserAction(_current, Action.Login);
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

        protected void OnUserAction(IUser user, Action action)
        {
            UserAction?.Invoke(this, new UserActionEventArgs(user, action));
        }

        public void Login(IUser user)
        {
            if (user == null
             || user.GetCredentials() == null)
                throw new SecurityException("User and/or credentials cannot be null");
            else if (!user.GetCredentials().Check())
                throw new SecurityException("Invalid user and/or credentials");

            Current = user;
        }

        public void Logout()
        {
            Current = null;
        }

        public static UserManager GetInstance()
        {
            return instance ?? (instance = new UserManager());
        }

        #endregion
    }
}
