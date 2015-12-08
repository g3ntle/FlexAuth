using System;

namespace FlexAuth.Security.Spoofing
{
    public class SpoofException : Exception
    {
        #region Constants

        private const string DefaultMessage = "Unable to spoof";

        #endregion


        #region Constructors

        public SpoofException()
            : base(DefaultMessage)
        { }

        public SpoofException(string message)
            : base(message)
        { }

        public SpoofException(Exception innerException)
            : base(DefaultMessage, innerException)
        { }

        public SpoofException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion
    }
}
