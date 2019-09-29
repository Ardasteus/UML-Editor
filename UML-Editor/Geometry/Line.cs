using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;

namespace UML_Editor.Geometry
{
    public struct Line
    {
        public Vector StartPoint { get; set; }
        public Vector EndPoint { get; set; }
        public Line(Vector start, Vector end)
        {
            StartPoint = start;
            EndPoint = end;
        }
    }
}
