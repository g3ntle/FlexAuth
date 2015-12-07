using System;

namespace FlexAuth.Security
{
    public class SignInException : AuthException
    {
        #region Constructors

        public SignInException(string message, int errorCode)
            : base(message, errorCode)
        { }

        public SignInException(string message, int errorCode, Exception innerException)
            : base(message, errorCode, innerException)
        { }

        #endregion
    }
}
