using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Nodes
{
    public class ButtonNode : IRenderableNode, IMouseHandlerNode
    {
        public ButtonNode(string name, Vector position, int width, int height, Action buttonAction, RectangleRenderElementStyle style)
        { 
            Name = name;
            Position = position;
            Width = width;
            Height = height;
            ButtonRectangle = new RectangleRenderElement(position, width, height, style.FillColor, style.BorderColor, style.BorderWidth);
            ButtonAction = buttonAction;
        }

        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isFocused { get; set; } = false;

        public Action ButtonAction { get; private set; }

        private RectangleRenderElement ButtonRectangle;

        public void HandleMouse()
        {
            ButtonAction.Invoke();
        }

        public void Render(Renderer renderer)
        {
            ButtonRectangle.Render(renderer);
        }
    }
}
