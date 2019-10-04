using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.ProjectStructure;

namespace UML_Editor.EventArguments
{
    public class CodeStructureEventArgs : EventArgs
    {
        public BasicCodeStructure CodeStructure { get; }

        public CodeStructureEventArgs(BasicCodeStructure structure)
        {
            CodeStructure = structure;
        }
    }
}
