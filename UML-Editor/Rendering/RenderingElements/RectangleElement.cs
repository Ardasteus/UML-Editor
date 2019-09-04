using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Rendering.RenderingElements
{
    public class RectangleElement : IRenderElement
    {
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Pen DrawPen { get; private set; }
        public SolidBrush FillBrush { get; private set; }
        private Color pencolor;
        public Color PenColor
        {
            get
            {
                return pencolor;
            }
            set
            {
                pencolor = value;
                DrawPen.Color = pencolor;
            }
        }
        private Color fillcolor;
        public Color FillColor
        {
            get
            {
                return fillcolor;
            }
            set
            {
                fillcolor = value;
                FillBrush.Color = fillcolor;
            }
        }
        private int penwidth;
        public RectangleElement()
        {

        }
        public void Render(Renderer renderer)
        {

        }
    }
}
