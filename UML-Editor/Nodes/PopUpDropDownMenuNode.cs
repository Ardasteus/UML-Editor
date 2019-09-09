using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class PopUpDropDownMenuNode : IRenderableNode, IKeyboardHandlerNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isFocused { get; set; } = false;    

        public void Render(Renderer renderer)
        {

        }

        public void HandleKey(char key)
        {
            
        }

        public void HandleMouse()
        {
            
        }

        public void ForceResize(int width)
        {
            throw new NotImplementedException();
        }
    }
}
