using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexAuth.Security
{
    public class NodeRepositoryBase : INodeRepository
    {
        #region Properties

        public virtual IEnumerable<string> Nodes { get; set; }

        #endregion

        #region Methods

        public virtual bool HasNode(string node)
        {
            return Nodes?.Contains(node) ?? false;
        }


        #endregion
    }
}
