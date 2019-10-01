using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.EventArguments
{
    public class NodeEventArgs : EventArgs
    {
        public INode Node { get; }

        public NodeEventArgs(INode node)
        {
            Node = node;
        }
    }
}
