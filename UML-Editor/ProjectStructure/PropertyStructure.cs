using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;

namespace UML_Editor.ProjectStructure
{
    public class PropertyStructure : BasicStructure
    {
        public PropertyStructure(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            Name = name;
            Type = type;
            AccessModifier = accessModifier;
            Modifier = modifier;
        }
        public string Type { get; set; }
    }
}
