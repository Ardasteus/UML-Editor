using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Geometry;

namespace UML_Editor.Relationships
{
    public class RelationshipSegment : IRenderableNode
    {
        public string Name { get; set; }
        public Vector Position
        {
            get => LineElement.StartPoint;
            set => LineElement.StartPoint = value;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        private LineRenderElement LineElement;
        public Vector AnchorPosition
        {
            get => LineElement.EndPoint;
            set => LineElement.EndPoint = value;
        }

        public RelationshipSegment(Vector position, Vector anchor)
        {
            LineElement = new LineRenderElement(position, anchor, 1, Color.Black);
        }
        public void Render(Renderer renderer)
        {
            //LineElement.Render(renderer);
        }
    }
}
