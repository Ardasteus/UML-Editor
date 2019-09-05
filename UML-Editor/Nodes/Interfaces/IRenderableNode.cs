using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public interface IRenderableNode : INode
    {
        List<IRenderElement> RenderElements { get; set; }
        void Generate();
    }
}
