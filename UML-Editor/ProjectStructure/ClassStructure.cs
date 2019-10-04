using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;

namespace UML_Editor.ProjectStructure
{
    public class ClassStructure : BasicCodeStructure
    {
        public List<MethodStructure> Methods { get; set; } = new List<MethodStructure>();
        public List<PropertyStructure> Properties { get; set; } = new List<PropertyStructure>();

        public void AddProperty(PropertyStructure prop) => Properties.Add(prop);
        public void AddMethod(MethodStructure method) => Methods.Add(method);

        public ClassStructure(Vector position, string name, string type, AccessModifiers accessModifier, Modifiers modifier) : base(position, name, type, accessModifier, modifier)
        {
        }
    }
}
