using System;

namespace FlexAuth.Security
{
    public class UserEventArgs
    {
        #region Constructors

        public UserEventArgs(IUser user)
        {
            User = user;
        }

        #endregion


        #region Properties

        public IUser User { get; set; }

        #endregion
    }
}
