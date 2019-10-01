using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.Nodes
{
    public interface IOptionsNode : IMouseFocusableNode
    {
        BasicContainerNode OptionsPrefab { get; set; }
        BasicContainerNode OptionsMenu { get; set; }
        EventHandler OnOptionsShow { get; set; }
        EventHandler OnOptionsHide { get; set; }
    }
}
