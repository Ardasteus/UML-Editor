using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    interface IParentNode : INode
    {
        List<IChildNode> Children { get; set; }
        List<IRenderElement> GetAllRenderElements();
    }
}
