using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UML_Editor
{
    public class ResizeEventArgs : EventArgs
    {
        public float Width { get; }
        public float Height { get; }
        public ResizeEventArgs(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
