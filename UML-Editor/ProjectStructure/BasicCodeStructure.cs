using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;

namespace UML_Editor.ProjectStructure
{
    public class BasicCodeStructure : BasicStructure
    {
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }
        public string Type { get; set; }
        public BasicCodeStructure(Vector position, string name, string type, AccessModifiers accessModifier, Modifiers modifier) : base(position, name)
        {
            AccessModifier = accessModifier;
            Modifier = modifier;
            Type = type;
        }
    }
}
