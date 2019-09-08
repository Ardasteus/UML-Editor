using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Rendering.ElementStyles
{
    public class TextRenderElementStyle
    {
        public int FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public string FontName { get; set; }
        public Color Color { get; set; }

        public TextRenderElementStyle(Font font, Color color)
        {
            FontSize = (int)font.Size;
            FontStyle = font.Style;
            FontName = font.Name;
            Color = color;
        }

        public TextRenderElementStyle(int fontSize, FontStyle fontStyle, string fontName, Color color)
        {
            FontSize = fontSize;
            FontStyle = fontStyle;
            FontName = fontName;
            Color = color;
        }

        public TextRenderElementStyle Default = new TextRenderElementStyle(12, FontStyle.Regular, "Arial", Color.Black);
    }
}
