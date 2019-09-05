using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class PopUpDropDownMenu : IRenderableNode, IMouseHandlerNode, IKeyboardHandlerNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public List<INode> ChildNodes { get; set; }
        public int TriggerAreaWidth { get; set; }
        public int TriggerAreaHeight { get; set; }
        public bool isFocused { get; set; } = false;

        public void Generate()
        {
            
        }
        public void Render(Renderer renderer)
        {

        }

        public void HandleKey()
        {
            
        }

        public void HandleMouse()
        {
            
        }
    }
}
