using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.EventArguments;

namespace UML_Editor.Nodes
{
    public interface IFocusableNode : IRenderableNode
    {
        EventHandler<NodeEventArgs> OnFocused { get; set; }
        EventHandler<NodeEventArgs> OnUnfocused { get; set; }
    }
}
