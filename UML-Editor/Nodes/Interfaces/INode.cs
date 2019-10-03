using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.EventArguments;
using UML_Editor.Geometry;
using UML_Editor.Hitboxes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public interface INode
    {
        Vector Position { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        List<IHitbox> TriggerAreas { get; set; }
        EventHandler<PositionEventArgs> OnPositionChanged { get; set; }
        EventHandler<ResizeEventArgs> OnResize { get; set; }
        EventHandler OnChange { get; set; }
        EventHandler<NodeEventArgs> OnRemoval { get; set; }
    }
}
