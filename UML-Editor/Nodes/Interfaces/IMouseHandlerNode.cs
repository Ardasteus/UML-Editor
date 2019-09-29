     using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;

namespace UML_Editor.Nodes
{
    public interface IMouseHandlerNode : IHandlerNode
    {
        void HandleMouse();
    }
}
