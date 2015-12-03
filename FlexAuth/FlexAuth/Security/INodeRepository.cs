using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexAuth.Security
{
    public interface INodeRepository
    {
        #region Properties

        IEnumerable<string> Nodes { get; set; }

        #endregion


        #region Methods

        bool HasNode(string node);

        #endregion
    }
}
