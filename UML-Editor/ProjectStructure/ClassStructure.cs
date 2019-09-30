using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;

namespace UML_Editor.ProjectStructure
{
    public class ClassStructure : BasicStructure
    {
        public ClassStructure(string name, AccessModifiers accessModifier, Modifiers modifier)
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
