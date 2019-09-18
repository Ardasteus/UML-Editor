using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering.ElementStyles;
using System.Drawing;
using UML_Editor.Others;

namespace UML_Editor.Nodes
{
    public class ButtonNode : IRenderableNode, IMouseHandlerNode
    {
        public ButtonNode(string name, string text, Vector position, int width, int height, Action buttonAction, RectangleRenderElementStyle style)
        { 
            Name = name;
            BorderElement = new RectangleRenderElement(position, width, height, style.FillColor, style.BorderColor, style.BorderWidth);
            TextElement = new TextRenderElement(new Vector(position.X, position.Y), text, Color.Black, 12);
            TriggerAreas.Add(new RectangleHitbox(position, Width, Height));
            ButtonAction = buttonAction;
        }

        public string Name { get; set; }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                TextElement.Position = value;
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
                OnResize?.Invoke();
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
        public bool isFocused { get; set; } = false;
        public string Text
        {
            get => TextElement.Text;
            set => TextElement.Text = value;
        }

        public Action ButtonAction { get; private set; }

        public Action OnResize { get; set; }
        public Action OnFocused { get; set; }
        public Action OnUnfocused { get; set; }

        private RectangleRenderElement BorderElement;
        private TextRenderElement TextElement;

        public void HandleMouse()
        {
            ButtonAction.Invoke();
        }

        public void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            TextElement.Render(renderer);
        }

        public void ForceResize(int width)
        {
        }
    }
}
