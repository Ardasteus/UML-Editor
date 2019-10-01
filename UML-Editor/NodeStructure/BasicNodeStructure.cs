using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;

namespace UML_Editor.NodeStructure
{
    public class BasicNodeStructure
    {
        public Vector Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public BasicNodeStructure(Vector pos, float width, float height)
        {
            Position = pos;
            Width = width;
            Height = height;
        }
    }
}
