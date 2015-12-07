using System;

namespace FlexAuth.Security.Generic
{
    public class DummyCredentials : ICredentials
    {
        #region Properties

        public object MetaData { get; set; }

        #endregion


        #region Methods

        public bool Check()
        {
            return true;
        }

        #endregion
    }
}
