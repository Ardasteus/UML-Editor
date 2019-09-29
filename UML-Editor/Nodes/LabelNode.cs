using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Geometry;

namespace UML_Editor.Nodes
{
    public class LabelNode : IRenderableNode
    {
        public string Name { get; set; }
        public string Text
        {
            get => TextElement.Text;
            set
            {
                TextElement.Text = value;
                Width = Renderer.GetTextWidth(Text.Length);
            }
        }
        public Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                TextElement.Position = value;
            }
        }
        public int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
                OnResize?.Invoke(this, new ResizeEventArgs(Width));
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
        public Color BorderColor
        {
            get => BorderElement.BorderColor;
            set => BorderElement.BorderColor = value;
        }
        public Color FillColor
        {
            get => BorderElement.FillColor;
            set => BorderElement.FillColor = value;
        }
        public int BorderWidth
        {
            get => BorderElement.BorderWidth;
            set => BorderElement.BorderWidth = value;
        }
        public Color TextColor
        {
            get => TextElement.Color;
            set => TextElement.Color = value;
        }
        public FontStyle TextStyle
        {
            get => TextElement.FontStyle;
            set => TextElement.FontStyle = value;
        }
        public int TextSize
        {
            get => TextElement.FontSize;
            set => TextElement.FontSize = value;
        }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        private RectangleRenderElement BorderElement;
        private TextRenderElement TextElement;

        public LabelNode(string name, string text, Vector position)
        {
            Name = name;
            BorderElement = new RectangleRenderElement(position, Renderer.GetTextWidth(text.Length), Renderer.SingleTextHeight, Color.White, Color.White);
            TextElement = new TextRenderElement(position, text, Color.Black);
            TextSize = 12;
        }


        public void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            TextElement.Render(renderer);
        }

        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
    }
}
