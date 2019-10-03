using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Rendering.ElementStyles
{
    public class RectangleRenderElementStyle
    {
        public RectangleRenderElementStyle(Color borderColor, Color fillColor, int borderWidth)
        {
            BorderColor = borderColor;
            FillColor = fillColor;
            BorderWidth = borderWidth;
        }

        public Color BorderColor { get; set; }
        public Color FillColor { get; set; }
        public int BorderWidth { get; set; }

        public static RectangleRenderElementStyle Default = new RectangleRenderElementStyle(Color.Black, Color.Lavender, 1);
        public static RectangleRenderElementStyle Classes = new RectangleRenderElementStyle(Color.Black, Color.White, 1);
        public static RectangleRenderElementStyle Textbox = new RectangleRenderElementStyle(Color.White, Color.White, 1);
    }
}
