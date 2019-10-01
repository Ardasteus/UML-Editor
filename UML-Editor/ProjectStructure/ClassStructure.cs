using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;

namespace UML_Editor.ProjectStructure
{
    public class ClassStructure : BasicStructure
    {
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }
        public ClassStructure(Vector position, string name, AccessModifiers accessModifier, Modifiers modifier) : base(position, name)
        {
            Name = name;
            AccessModifier = accessModifier;
            Modifier = modifier;
        }


        public List<MethodStructure> Methods { get; set; }
        public List<PropertyStructure> Properties { get; set; }

        public void AddProperty(PropertyStructure prop) => Properties.Add(prop);
        public void AddMethod(MethodStructure method) => Methods.Add(method);
    }
}
