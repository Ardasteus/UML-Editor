using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UML_Editor.Rendering.RenderingElements
{
    public class LineElement : IRenderElement
    {
        public Vector StartPoint { get; set; }
        public Vector EndPoint { get; set; }
        public Pen DrawPen { get; private set; }
        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                DrawPen.Width = width; 
            }
        }
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
        private DashStyle dashstyle;
        public DashStyle DashStyle
        {
            get
            {
                return dashstyle;
            }
            set
            {
                dashstyle = value;
                DrawPen.DashStyle = dashstyle;
            }
        }
        private LineCap startcap;
        public LineCap StartCap
        {
            get
            {
                return startcap;
            }
            set
            {
                startcap = value;
                DrawPen.StartCap = startcap;
            }
        }
        private LineCap endcap;
        public LineCap EndCap
        {
            get
            {
                return endcap;
            }
            set
            {
                endcap = value;
                DrawPen.EndCap = endcap;
            }
        }

        public LineElement(Vector from, Vector to, int width, Color color)
        {
            StartPoint = from;
            EndPoint = to;
            Width = width;
            DrawPen = new Pen(color, width);
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawLine(StartPoint, EndPoint, DrawPen);
        }
    }
}
