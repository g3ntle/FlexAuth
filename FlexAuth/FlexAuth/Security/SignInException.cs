using System;

namespace FlexAuth.Security
{
    public class SignInException : AuthException
    {
        #region Cosntructors

        public SignInException()
            : base()
        { }

        public SignInException(string message)
            : base(message)
        { }

        public SignInException(string message, int errorCode)
            : base(message, errorCode)
        { }

        public SignInException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public SignInException(string message, int errorCode, Exception innerException)
            : base(message, errorCode, innerException)
        { }

        #endregion
    }
}
