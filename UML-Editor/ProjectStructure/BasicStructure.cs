using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;

namespace UML_Editor.ProjectStructure
{
    public abstract class BasicStructure
    {
        public string Name { get; set; }
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }
    }
}
