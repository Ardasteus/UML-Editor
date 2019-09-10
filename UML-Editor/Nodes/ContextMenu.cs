using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class ContextMenu : IRenderableNode, IKeyboardHandlerNode, IContainerNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width
        {
            get => MenuRectangle.Width;
            set
            {
                MenuRectangle.Width = value;
                ForceResize(Width);
            }
        }
        public int Height
        {
            get => MenuRectangle.Height;
            set => MenuRectangle.Height = value;
        }
        public bool isFocused { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Resize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private List<INode> ChildNodes = new List<INode>();
        private RectangleRenderElement MenuRectangle;

        public ContextMenu(string name, Vector position, int width, int height, RectangleRenderElementStyle style)
        {
            Name = name;
            Position = position;
            MenuRectangle = new RectangleRenderElement(position, width, height, style.FillColor, style.BorderColor, style.BorderWidth);
        }

        public void AddNode(INode node)
        {
            ChildNodes.Add(node);
        }

        public void HandleKey(char key)
        {
;
        }

        public void Render(Renderer renderer)
        {
            MenuRectangle.Render(renderer);
            ChildNodes.OfType<IRenderableNode>().ToList().ForEach(x => x.Render(renderer));
        }

        public void ForceResize(int width)
        {
            ChildNodes.ForEach(x => x.ForceResize(width));
        }

        public List<INode> GetChildren()
        {
            return ChildNodes;
        }
    }
}
