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
    public class RelationshipSegment : IRenderableNode, IOptionsNode
    {
        public string Name { get; set; }
        public Vector Position
        {
            get => SegmentStart.StartPoint;
            set => SegmentStart.StartPoint = value;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        private LineRenderElement SegmentStart;
        private LineRenderElement SegmentEnd;
        public Vector JointPosition
        {
            get => SegmentStart.EndPoint;
            set
            {
                SegmentStart.EndPoint = value;
                SegmentEnd.StartPoint = value;
            }
        }
        public Vector AnchorPosition
        {
            get => SegmentEnd.EndPoint;
            set => SegmentEnd.EndPoint = value;
        }
        public ContextMenuNode OptionsPrefab { get; set; }
        public ContextMenuNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }

        public RelationshipSegment(Vector position, Vector joint, Vector anchor)
        {
            SegmentStart = new LineRenderElement(position, joint, 1, Color.Black);
            SegmentEnd = new LineRenderElement(joint, anchor, 1, Color.Black);
            TriggerAreas.Add(new RectangleHitbox((position + joint) / 2, (int)(position.X + joint.X) / 2, (int)(position.Y + joint.Y) / 2));
            TriggerAreas.Add(new RectangleHitbox((anchor + joint) / 2, (int)(anchor.X + joint.X) / 2, (int)(anchor.Y + joint.Y) / 2));
        }
        public void Render(Renderer renderer)
        {
            SegmentStart.Render(renderer);
            SegmentEnd.Render(renderer);
        }

        public void ShowOptionsMenu()
        {
        }

        public void HandleMouse()
        {
        }
    }
}
