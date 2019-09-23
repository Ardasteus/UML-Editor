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
        public ClassDiagramNode TargetNode { get; set; }
        public Vector AnchorPosition
        {
            get => LineElement.EndPoint;
            set => LineElement.EndPoint = value;
        }

        public RelationshipSegment(Vector position, Vector anchor, ClassDiagramNode classDiagram)
        {
            TargetNode = classDiagram;
            LineElement = new LineRenderElement(position, anchor, 1, Color.Black);
            TargetNode.OnPositionChanged += OnNodePositionChanged;
        }

        public void Render(Renderer renderer)
        {
            LineElement.Render(renderer);
        }
        private void OnNodePositionChanged(object sender, PositionEventArgs e)
        {
            OnAnchorRequest?.Invoke(this, new EventArgs());
        }

        public EventHandler OnAnchorRequest;

        public void SetRelationshipType()
        {
            LineElement.EndCap = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
        }
    }
}
