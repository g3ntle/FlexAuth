using System;
using System.Collections.Generic;

namespace FlexAuth.Security.Generic
{
    public class DummyNodeRepository : INodeRepository
    {
        public IEnumerable<string> Nodes { get; set; }

        public bool HasNode(string node)
        {
            return true;
        }
    }
}
