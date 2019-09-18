using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public interface INode
    {
        string Name { get; set; }
        Vector Position { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        List<IHitbox> TriggerAreas { get; set; }
        Action OnResize { get; set; }
        Action OnFocused { get; set; }
        Action OnUnfocused { get; set; }
    }
}
