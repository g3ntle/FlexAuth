using Beans;
using System;

namespace FlexAuth.Security.Spoofing
{
    [Bean]
    public sealed class SpoofArgs
    {
        #region Properties

        [BeanProperty]
        public string UserType { get; set; }
        [BeanProperty]
        public string CredentialsType { get; set; }

        #endregion
    }
}
