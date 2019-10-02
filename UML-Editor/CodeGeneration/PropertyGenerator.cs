using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.ProjectStructure;

namespace UML_Editor.CodeGeneration
{
    public class PropertyGenerator : BaseGenerator
    {
        public PropertyStructure Structure { get; set; }

        public PropertyGenerator(PropertyStructure structure)
        {
            Structure = structure;
            GenerateLines();
        }

        private void GenerateLines()
        {
            string line = "        public " + Structure.Type + " " + Structure.Name + " { get; set; }";
            Lines.Add(line);
        }
    }
}
