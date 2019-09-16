using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class ContextMenuNode : IRenderableNode, IKeyboardHandlerNode, IContainerNode
    {
        public string Name { get; set; }
        public Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
            }
        }
        public int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
            }
        }
        public int Height
        {
            get => BorderElement.Height;
            set
            {
                BorderElement.Height = value;
                ((RectangleHitbox)TriggerAreas[0]).Height = value;
            }
        }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public bool isFocused { get; set; }
        public bool Resize { get; set; }

        private List<INode> ChildNodes = new List<INode>();
        private RectangleRenderElement BorderElement;

        public ContextMenuNode(string name, Vector position, int width, int height, RectangleRenderElementStyle style)
        {
            Name = name;
            BorderElement = new RectangleRenderElement(position, width, height, style.FillColor, style.BorderColor, style.BorderWidth);
            TriggerAreas.Add(new RectangleHitbox(position, Width, Height));
        }

        public void AddNode(INode node)
        {
            node.Position = new Vector(Position.X, Position.Y + ChildNodes.Count * Renderer.SingleTextHeight);
            node.ForceResize(Width);
            Height += Renderer.SingleTextHeight;
            ChildNodes.Add(node);
        }

        public void HandleKey(char key)
        {
;
        }

        public void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            ChildNodes.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(renderer));
        }

        public void ForceResize(int width)
        {
            BorderElement.Width = width;
            ChildNodes.ForEach(x => x.ForceResize(width));
        }

        public List<INode> GetChildren()
        {
            return ChildNodes;
        }
    }
}
