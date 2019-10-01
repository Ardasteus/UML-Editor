using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;
using UML_Editor.Rendering;

namespace UML_Editor
{
    public class PositionEventArgs : EventArgs
    {
        public Vector Position;
        public PositionEventArgs(Vector position)
        {
            Position = position;
        }
    }
}
