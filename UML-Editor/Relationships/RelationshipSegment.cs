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

namespace UML_Editor.Relationships
{
    public class RelationshipSegment : IRenderableNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        private LineRenderElement LineElement;
        public ClassDiagramNode TargetNode { get; set; }

        public RelationshipSegment(Vector position, ClassDiagramNode classDiagram)
        {
            Position = position;
            TargetNode = classDiagram;
            LineElement = new LineRenderElement(position, classDiagram.Position, 1, Color.Black);
            TargetNode.OnPositionChanged += OnPositionChanged;
        }

        public void Render(Renderer renderer)
        {
            LineElement.Render(renderer);
        }
        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            LineElement = new LineRenderElement(Position, e.Position, 1, Color.Black);
        }
    }
}
