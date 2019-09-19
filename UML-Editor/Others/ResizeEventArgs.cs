using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Others
{
    public class ResizeEventArgs : EventArgs
    {
        public int Width;
        public ResizeEventArgs(int width)
        {
            Width = width;
        }
    }
}
