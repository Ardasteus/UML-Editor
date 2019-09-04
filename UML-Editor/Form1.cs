using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Plane.Image = new Bitmap(Plane.Width, Plane.Height);
            Renderer plane = new Renderer(Plane);
            RectangleElement rectangle = new RectangleElement(new Vector(0, 0), 10, 20, Color.White, Color.Black);
            LineElement line = new LineElement(new Vector(0, 0), new Vector(20, 20), 5, Color.Aqua);
            TextElement text = new TextElement(new Vector(10, 10), "Hello World", Color.Black);
            rectangle.Render(plane);
            line.Render(plane);
            text.Render(plane);
            plane.Render();
        }
    }
}
