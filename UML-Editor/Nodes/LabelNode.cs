using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;

namespace UML_Editor.Nodes
{
    public class LabelNode : IRenderableNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Resize { get; set; }

        public void ForceResize(int width)
        {
        }

        public void Render(Renderer renderer)
        {
        }
    }
}
