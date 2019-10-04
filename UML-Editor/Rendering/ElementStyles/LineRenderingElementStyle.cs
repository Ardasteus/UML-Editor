using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Rendering.ElementStyles
{
    public class LineRenderingElementStyle
    {
        public LineRenderingElementStyle(int width, Color color, DashStyle dashStyle = DashStyle.Solid, LineCap startCap = LineCap.NoAnchor, LineCap endCap = LineCap.NoAnchor)
        {
            Width = width;
            Color = color;
            DashStyle = dashStyle;
            StartCap = startCap;
            EndCap = endCap;
        }

        public int Width { get; set; }
        public Color Color { get; set; }
        public DashStyle DashStyle { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }

        public LineRenderingElementStyle Default = new LineRenderingElementStyle(1, Color.Black);
    }
}
