using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexAuth.Security
{
    public class UserActionEventArgs
    {
        #region Constructors

        public UserActionEventArgs(IUser user, Action action)
        {
            User = user;
            Action = action;
        }

        #endregion


        #region Properties

        public IUser User { get; set; }
        public Action Action { get; set; }

        #endregion
    }
}
