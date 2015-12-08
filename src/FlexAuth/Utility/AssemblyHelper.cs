using System;
using System.Linq;
using System.Reflection;

namespace FlexAuth.Utility
{
    public static class AssemblyHelper
    {
        #region Methods

        public static Type GetTypeByName(this Assembly asm, string typeName)
        {
            return asm.GetTypes()
                .FirstOrDefault(t => t.Name == typeName);
        }

        #endregion
    }
}
