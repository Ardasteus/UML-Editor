using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using System.Windows.Forms;

namespace UML_Editor.Nodes
{
    public class TextBoxNode : IRenderableNode, IKeyboardHandlerNode, IMouseHandlerNode
    {
        public TextBoxNode(string name, string text, Vector position, int width, int height, Color text_color, Color border_color, Color fill_color, int border_width = 1)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Text = text;
            BorderElement = new RectangleRenderElement(position, width, height, fill_color, border_color, border_width);
            TextElement = new TextRenderElement(Position, text, text_color);
            TextSize = 12;
        }

        public bool Resize = true;
        public string Name { get; set; }
        public string Text { get; set; }
        public Vector Position { get; set; }
        public int Width
        {
            get => BorderElement.Width;
            set => BorderElement.Width = value;
        }
        public int Height
        {
            get => BorderElement.Height;
            set => BorderElement.Height = value;
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
            if (Resize)
                ForceResize();
            else
                GetDrawnText();

            if (isFocused)
                FillColor = Color.CornflowerBlue;
            else
                FillColor = Color.White;

            BorderElement.Render(renderer);
            TextElement.Render(renderer);
        }

        public void HandleKey(char key)
        {
            if (key == (char)8)
                Text = Text.Substring(0, Text.Length - 1);
            else if (Char.IsWhiteSpace(key))
                Text = Text.Insert(Text.Length, " ");
            else if (Char.IsLetter(key))
                Text = Text.Insert(Text.Length, key.ToString());
        }

        public void HandleMouse()
        {

        }

        private void ForceResize()
        {
            Width = 13 + (Text.Length - 1) * 9;
            TextElement.Text = Text;
        }

        private void GetDrawnText()
        {
            int range = 0;
            for (int i = 13; i <= Width; i += Renderer.TextWidthGap)
            {
                range++;
            }
            TextElement.Text = Text.Substring(0, range);
        }
    }
}
