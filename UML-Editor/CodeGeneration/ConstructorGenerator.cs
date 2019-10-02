using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.ProjectStructure;

namespace UML_Editor.CodeGeneration
{
    public class ConstructorGenerator : BaseGenerator
    {
        public List<PropertyStructure> Properties { get; set; }
        public ClassStructure Class { get; set; }
        public ConstructorGenerator(ClassStructure klass, List<PropertyStructure> props)
        {
            Properties = props;
            Class = klass;
            GenerateLines();
        }

        private void GenerateLines()
        {
            string Arguments = "";
            foreach (PropertyStructure prop in Properties)
            {
                Arguments += prop.Name + " ";
                Arguments += prop.Type + ", ";
            }

            if (Arguments.Length > 1)
                Arguments = Arguments.Substring(0, Arguments.Length - 2);
            string line = "        public " + Class.Name + "(" + Arguments + ")";
            Lines.Add(line);
            Lines.Add("        {");
            Lines.Add("        }");
        }
    }
}
