using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.Nodes
{
    public interface IOptionsNode : IMouseHandlerNode
    {
        ContextMenuNode OptionsPrefab { get; set; }
        ContextMenuNode OptionsMenu { get; set; }
        void ShowOptionsMenu();
    }
}
