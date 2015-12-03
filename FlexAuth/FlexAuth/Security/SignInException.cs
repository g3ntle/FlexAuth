using System;

namespace FlexAuth.Security
{
    public class SignInException : AuthException
    {
        public SignInException()
            : base()
        { }

        public SignInException(string message)
            : base(message)
        { }

        public SignInException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
