using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering;

namespace UML_Editor.Nodes
{
    public interface IRenderableNode : INode
    {
        void Render(Renderer renderer);
    }
}
