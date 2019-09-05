using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Nodes
{
    public interface IMouseHandlerNode : IHandlerNode
    {
        void HandleMouse();
        int TriggerAreaWidth { get; set; }
        int TriggerAreaHeight { get; set; }
    }
}
