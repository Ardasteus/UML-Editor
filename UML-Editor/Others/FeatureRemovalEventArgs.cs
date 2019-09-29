using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.Others
{
    public class FeatureRemovalEventArgs : EventArgs
    {
        public FeatureNode FeatureNode { get; set; }
        public FeatureRemovalEventArgs(FeatureNode node)
        {
            FeatureNode = node;
        }
    }
}
