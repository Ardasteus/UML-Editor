using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UML_Editor.Rendering.RenderingElements
{
    public class TextRenderElement : IRenderElement
    {
        public Vector Position { get; set; } 
        public string Text { get; set; }
        public Font Font { get; private set; }
        private SolidBrush Brush { get; set; }
        private int fontSize;
        public int FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                Font = new Font(Font.FontFamily, fontSize, FontStyle);
            }
        }
        private FontStyle fontStyle;
        public FontStyle FontStyle
        {
            get
            {
                return fontStyle;
            }
            set
            {
                fontStyle = value;
                Font = new Font(Font, fontStyle);
            }
        }
        private string fontName;
        public string FontName
        {
            get
            {
                return fontName;
            }
            set
            {
                string temp = fontName;
                try
                {
                    Font = new Font(fontName, FontSize, fontStyle);
                    fontName = value;
                }
                catch
                {
                    fontName = temp;
                }
            }
        }
        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                Brush.Color = color;
            }
        }

        public TextRenderElement(Vector position, string text, Color text_color, string font = "Arial", int size = 14, FontStyle style = FontStyle.Regular)
        {
            Font = new Font(font, size, style);
            Brush = new SolidBrush(text_color);
            Position = position;
            Text = text;
            Color = text_color;
            FontName = font;
            FontSize = size;
            FontStyle = style;
        }
        public void Render(Renderer renderer)
        {
            renderer.DrawText(Position, Text, Font, Brush);
        }
    }
}
