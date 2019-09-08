using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UML_Editor.Nodes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
namespace UML_Editor
{
    public partial class Form1 : Form
    {
        TextBoxNode textBox;
        Renderer renderer;
        TextRenderElement count1;
        TextRenderElement count2;
        public Form1()
        {
            InitializeComponent();
            Plane.Image = new Bitmap(Plane.Width, Plane.Height);
            renderer = new Renderer(Plane);
            textBox = new TextBoxNode("txt1", "OOOOOO", Vector.Zero, 22, 18, Color.Black, Color.Black, Color.White);
            LineRenderElement line1 = new LineRenderElement(Vector.Zero - new Vector(Plane.Width / 2, 0), Vector.Zero + new Vector(Plane.Width / 2, 0), 1, Color.Black);
            LineRenderElement line2 = new LineRenderElement(Vector.Zero - new Vector(0, Plane.Height / 2), Vector.Zero + new Vector(0, Plane.Height / 2), 1, Color.Black);
            textBox.TextStyle = FontStyle.Regular;
            count1 = new TextRenderElement(new Vector(-Plane.Width / 2, -Plane.Height / 2), "Text", Color.Black);
            count2 = new TextRenderElement(new Vector(-Plane.Width / 2, (-Plane.Height / 2) + 20), "Text", Color.Black);
            Thread render_task = new Thread(() =>
            {
                while(true)
                {
                    count1.Render(renderer);
                    count2.Render(renderer);
                    line1.Render(renderer);
                    line2.Render(renderer);
                    textBox.Render(renderer);
                    renderer.Render();
                }
            });
            render_task.Start();
        }

        private void Plane_MouseClick(object sender, MouseEventArgs e)
        {
            Vector vector = renderer.Origin - e.Location;
            count1.Text = vector.X.ToString();
            count2.Text = vector.Y.ToString();
            count1.Text = "Ths is a Test";
            //int left = textBox.Position.X;
            //int right = textBox.Position.X + textBox.Width;
            //int top = textBox.Position.Y;
            //int bot = textBox.Position.Y + textBox.Height / 2;
            //if (vector.X <= right && vector.X >= left && vector.Y <= bot && vector.Y <= top)
            //    textBox.isFocused = true;
        }
    }
}
