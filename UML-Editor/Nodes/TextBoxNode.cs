using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using System.Windows.Forms;
using UML_Editor.Others;
using UML_Editor.Geometry;

namespace UML_Editor.Nodes
{
    public class TextBoxNode : IRenderableNode, IKeyboardHandlerNode, IMouseHandlerNode
    {
        public TextBoxNode(string name, string text, Vector position, int width, int height, Color text_color, Color border_color, Color fill_color, int border_width = 1)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BorderElement = new RectangleRenderElement(position, width, height, fill_color, border_color, border_width);
            TextElement = new TextRenderElement(Position, text, text_color);
            TextSize = 12;
            TriggerAreas.Add(new RectangleHitbox(position, width, height));
            Text = text;
        }


        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
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
        public bool isFocused { get; set; } = false;
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

        private RectangleRenderElement BorderElement;
        private TextRenderElement TextElement;


        public void Render(Renderer renderer)
        {

            if (isFocused)
                FillColor = Color.CornflowerBlue;
            else
                FillColor = Color.White;
            GetDrawnText();
            BorderElement.Render(renderer);
            TextElement.Render(renderer);
        }

        public void HandleKey(char key)
        {
            if (key == (char)8 && Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
                Width = Renderer.GetTextWidth(Text.Length);
            }
            else if (key == (char)13)
                isFocused = false;
            else if (Char.IsWhiteSpace(key))
                Text = Text.Insert(Text.Length, " ");
            else
                Text = Text.Insert(Text.Length, key.ToString());
        }

        public void HandleMouse()
        {

        }

        private void GetDrawnText()
        {
            if (Renderer.GetTextWidth(Text.Length) > Width)
            {
                int range = 0;
                for (int i = 13; i <= Width; i += Renderer.TextWidthGap)
                {
                    range++;
                }
                if (isFocused)
                    TextElement.Text = Text.Substring(Text.Length - range, range);
                else
                    TextElement.Text = Text.Substring(0, range);
            }
            else
                TextElement.Text = Text;
        }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
    }
}
