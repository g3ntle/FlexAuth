using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexAuth.Security
{
    public class NodeRepositoryBase : INodeRepository
    {
        #region Constants

        private const string Wildcard = "*";

        #endregion


        #region Properties

        private IEnumerable<string> _nodes;
        public virtual IEnumerable<string> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                _hasWildcard = HasNode(Wildcard, true);
            }
        }

        private bool _hasWildcard;
        public virtual bool HasWildcard
        {
            get { return _hasWildcard; }
        }

        #endregion

        #region Methods

        public virtual bool HasNode(string node, bool ignore)
        {
            return ((ignore ? false : HasWildcard) ? true : Nodes?.Contains(node) ?? false);
        }

        public virtual bool HasNode(string node)
        {
            return HasNode(node, false);
        }

        #endregion
    }
}
