using System;

namespace FlexAuth.Security
{
    public class SignOutException : AuthException
    {
        #region Cosntructors

        public SignOutException()
            : base()
        { }

        public SignOutException(string message)
            : base(message)
        { }

        public SignOutException(string message, int errorCode)
            : base(message, errorCode)
        { }

        public SignOutException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public SignOutException(string message, int errorCode, Exception innerException)
            : base(message, errorCode, innerException)
        { }

        #endregion
    }
}
