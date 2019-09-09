﻿using System;
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
        Editor editor;
        public Form1()
        {
            InitializeComponent();
            Plane.Image = new Bitmap(Plane.Width, Plane.Height);
            editor = new Editor(Plane);
            this.KeyPress += editor.OnKeyPress;
            this.KeyDown += editor.OnKeyDown;
            this.KeyUp += editor.OnKeyUp;
            editor.AddNode(new TextBoxNode("txt1", "OOOOOO", Vector.Zero, 22, 18, Color.Black, Color.Black, Color.White));
            editor.Render();
        }
    }
}
