using System;

namespace FlexAuth.Security
{
    public class AuthException : Exception
    {
        #region Constructors

        public AuthException(string message, int errorCode)
            : base(message)
        {
            _errorCode = errorCode;
        }

        public AuthException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            _errorCode = errorCode;
        }

        #endregion


        #region Properties

        private int _errorCode;
        public int ErrorCode
        {
            get { return _errorCode; }
        }

        #endregion
    }
}
