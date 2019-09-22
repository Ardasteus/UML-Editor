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
        private Vector AnchorPosition;

        public RelationshipSegment(Vector position, ClassDiagramNode classDiagram)
        {
            Position = position;
            TargetNode = classDiagram;
            AnchorPosition = classDiagram.Position + new Vector(classDiagram.Width / 2, classDiagram.Height / 2);
            LineElement = new LineRenderElement(position, AnchorPosition, 1, Color.Black);
            TargetNode.OnPositionChanged += OnNodePositionChanged;
        }

        public void Render(Renderer renderer)
        {
            LineElement.Render(renderer);
        }
        private void OnNodePositionChanged(object sender, PositionEventArgs e)
        {
            AnchorPosition = TargetNode.Position + new Vector(TargetNode.Width / 2, TargetNode.Height / 2);
            LineElement = new LineRenderElement(Position, AnchorPosition, 1, Color.Black);
        }
    }
}
