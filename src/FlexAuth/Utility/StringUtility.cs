using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FlexAuth.Utility
{
    public static class StringUtility
    {
        #region Constants

        private static readonly Regex Expression = new Regex(@"\s*(.*?)\s*=\s*(.*?)\s*(;|$)");

        #endregion


        #region Methods

        public static Dictionary<string, object>GetValues(this string str)
        {
            if (str == null)
                return null;

            return Expression.Matches(str)
                .OfType<Match>()
                .ToDictionary(m => m.Groups[1].Value, m => (object)m.Groups[2].Value);
        }

        #endregion
    }
}
