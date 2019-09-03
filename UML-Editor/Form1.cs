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

namespace UML_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Plane.Image = new Bitmap(Plane.Width, Plane.Height);
            Renderer plane = new Renderer(Plane);
            plane.DrawLine(Point.Empty, new Point(100, 100), new Pen(Color.Red,20));
            plane.Render();
        }
    }
}
