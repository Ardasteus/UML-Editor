using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.EventArguments;

namespace UML_Editor.Nodes
{
    public interface IContainerNode : INode
    {
        List<INode> Children { get; set; }
        List<IFocusableNode> GetFocusableNodes();
        IFocusableNode FocusedNode { get; set; }
        EventHandler<NodeEventArgs> OnNodeAdd { get; set; }
        EventHandler<NodeEventArgs> OnNodeRemoval { get; set; }
    }
}
