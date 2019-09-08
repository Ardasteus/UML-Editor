using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace UML_Editor.Rendering
{
    public class Renderer
    {
        private PictureBox plane;
        public Vector Origin { get; private set; }
        private int width;
        private int height;

        public Renderer(PictureBox pic)
        {
            plane = pic;
            width = plane.Width;
            height = plane.Height;
            Origin = new Vector(width / 2, height / 2);
        }

        public void DrawPixer(Vector p, Pen draw_pen)
        {

        }

        public void DrawLine(Vector from, Vector to, Pen draw_pen)
        {
            using (Graphics g = GetGraphics())
            {
                g.DrawLine(draw_pen, from + Origin, to + Origin);
            }
        }

        public void DrawElipse(Vector position, int width, int height, Pen draw_pen, Brush fill_brush = null)
        {

        }

        public void DrawRectangle(Vector position, int width, int height, Pen draw_pen, Brush fill_brush = null)
        {
            Rectangle rect = new Rectangle(position.X + Origin.X, position.Y + Origin.Y, width, height);
            using (Graphics g = GetGraphics())
            {
                if (fill_brush != null)
                {
                    g.FillRectangle(fill_brush, rect);
                }
                g.DrawRectangle(draw_pen, rect);
            }
        }

        public void DrawText(PointF position, string text, Font font, Brush brush)
        {
            using (Graphics g = GetGraphics())
            {
                g.DrawString(text, font, brush, position + Origin);
            }
        }

        public void DrawTriangle(Vector A, Vector B, Vector C, Pen draw_pen)
        {

        }

        public void Render()
        {
            lock(plane)
            {
                plane.Refresh();
            }
        }

        private Graphics GetGraphics()
        {
            return Graphics.FromImage(plane.Image);
        }

        public static readonly int TextWidthGap = 9;
    }
}
