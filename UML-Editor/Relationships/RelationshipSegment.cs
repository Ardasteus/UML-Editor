using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Hitboxes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Geometry;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Relationships
{
    public class RelationshipSegment //: IRenderableNode
    {
        public string Name { get; set; }
        public Vector Position
        {
            get => SegmentStart.StartPoint;
            set
            {
                SegmentStart.StartPoint = value;
                CreateHitboxes();
            }
        }
        public float Width { get; set; }
        public float Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        private List<RectangleRenderElement> DebugRectangles = new List<RectangleRenderElement>();

        private LineRenderElement SegmentStart;
        private LineRenderElement SegmentEnd;
        public Vector Joint
        {
            get => SegmentStart.EndPoint;
            set
            {
                SegmentStart.EndPoint = value;
                SegmentEnd.StartPoint = value;
                CreateHitboxes();
            }
        }
        public Vector Midpoint
        {
            get => SegmentEnd.EndPoint;
            set
            {
                SegmentEnd.EndPoint = value;
                CreateHitboxes();
            }
        }
        public BasicContainerNode OptionsPrefab { get; set; }
        public BasicContainerNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }

        public RelationshipSegment(Vector position, Vector joint, Vector anchor)
        {
            SegmentStart = new LineRenderElement(position, joint, 1, Color.Black);
            SegmentEnd = new LineRenderElement(joint, anchor, 1, Color.Black);
            CreateHitboxes();
        }
        public void Render(Renderer renderer)
        {
            SegmentStart.Render(renderer);
            SegmentEnd.Render(renderer);
            OptionsMenu?.Render(renderer);
            DebugRectangles.ForEach(x => x.BorderOnly(renderer));
        }

        private void CreateHitboxes()
        {
            Vector position = SegmentStart.StartPoint;
            Vector joint = SegmentStart.EndPoint;
            Vector anchor = SegmentEnd.EndPoint;
            TriggerAreas = new List<IHitbox>();
            DebugRectangles = new List<RectangleRenderElement>();
            TriggerAreas.Add(RectangleHitbox.CreateFromLine(position, joint, 10));
            TriggerAreas.Add(RectangleHitbox.CreateFromLine(joint, anchor, 10));
            DebugRectangles.Add(RectangleRenderElement.CreateFromLine(joint, anchor, 10));
            DebugRectangles.Add(RectangleRenderElement.CreateFromLine(position, joint, 10));
        }
    }
}
