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
            //RectangleRenderElement rectangle = new RectangleRenderElement(Vector.Zero, 10, 20, Color.White, Color.Black);
            //LineRenderElement line = new LineRenderElement(Vector.Zero, new Vector(20, 20), 5, Color.Aqua);
            //TextRenderElement text = new TextRenderElement(Vector.Zero, "Hello World", Color.Black);
            foreach (IRenderElement item in classDiagram.GetAllRenderElements())
            {
                item.Render(plane);
            }
            plane.Render();
        }
    }
}
