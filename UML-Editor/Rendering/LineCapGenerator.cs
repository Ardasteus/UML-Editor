using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using UML_Editor.Geometry;

namespace UML_Editor.Rendering
{
    public static class LineCapGenerator
    {
        public static CustomLineCap DiamondCap(bool fill)
        {
            GraphicsPath path = new GraphicsPath();
            CustomLineCap Diamond;
            path.AddLine(new Vector(0, 0), new Vector(5, 5));
            path.AddLine(new Vector(5, 5), new Vector(10, 0));
            path.AddLine(new Vector(10, 0), new Vector(5, -5));
            path.AddLine(new Vector(5, -5), new Vector(0, 0));
            if (fill)
                Diamond = new CustomLineCap(null, path);
            else
                Diamond = new CustomLineCap(path, path);
            return Diamond;
        }
    }
}
