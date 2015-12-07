using System;

namespace FlexAuth.Security
{
    public class SignOutException : AuthException
    {
        #region Constructors

        public SignOutException(string message, int errorCode)
            : base(message, errorCode)
        { }

        public SignOutException(string message, int errorCode, Exception innerException)
            : base(message, errorCode, innerException)
        { }

        #endregion
    }
}
