using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Rendering.RenderingElements
{
    public class RectangleRenderElement : IRenderElement
    {
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Pen DrawPen { get; set; }
        private SolidBrush FillBrush { get; set; }
        private Color borderColor;
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                DrawPen.Color = borderColor;
            }
        }
        private Color fillColor;
        public Color FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor = value;
                FillBrush.Color = fillColor;
            }
        }
        private int borderWidth;
        public int BorderWidth
        {
            get
            {
                return borderWidth;
            }
            set
            {
                borderWidth = value;
                DrawPen.Width = borderWidth;
            }
        }
        public RectangleRenderElement(Vector position, int width, int height, Color fill_color, Color border_color, int border_width = 1)
        {
            DrawPen = new Pen(border_color, border_width);
            FillBrush = new SolidBrush(fill_color);
            Position = position;
            Width = width;
            Height = height;
            FillColor = fill_color;
            BorderColor = border_color;
            BorderWidth = border_width;
        }
        public void Render(Renderer renderer)
        {
            renderer.DrawRectangle(Position, Width, Height, DrawPen, FillBrush);
        }
        public void BorderOnly(Renderer renderer)
        {
            renderer.DrawRectangle(Position, Width, Height, DrawPen);
        }
    }
}
