using System;

namespace FlexAuth.Security
{
    public class SignOutException : AuthException
    {
        public SignOutException()
            : base()
        { }

        public SignOutException(string message)
            : base(message)
        { }

        public SignOutException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
