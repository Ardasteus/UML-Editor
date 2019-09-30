using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;

namespace UML_Editor.ProjectStructure
{
    public class MethodStructure : BasicStructure
    {
        public List<ArgumentStructure> Arguments;

        public MethodStructure(string name, string type, AccessModifiers accessModifier, Modifiers modifier, List<ArgumentStructure> arguments)
        {
            Name = name;
            Type = type;
            AccessModifier = accessModifier;
            Modifier = modifier;
            Arguments = arguments;
        }
        public MethodStructure(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            Name = name;
            Type = type;
            AccessModifier = accessModifier;
            Modifier = modifier;
            Arguments = new List<ArgumentStructure>();
        }

        public void AddArgument(string name, string type)
        {
            Arguments.Add(new ArgumentStructure(name, type));
        }
        public string Type { get; set; }
    }
}
