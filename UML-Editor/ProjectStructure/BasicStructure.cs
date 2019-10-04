using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;

namespace UML_Editor.ProjectStructure
{
    public abstract class BasicStructure
    {
        protected BasicStructure(Vector position, string name)
        {
            Position = position;
            Name = name;
        }

        public Vector Position { get; set; }
        public string Name { get; set; }
    }
}
