using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.ProjectStructure;

namespace UML_Editor.CodeGeneration
{
    public class CodeGenerator
    {
        public Project Project { get; set; }
        public string DirectoryPath { get; set; }
        List<ClassGenerator> Classes = new List<ClassGenerator>();

        public CodeGenerator(Project project, string dir)
        {
            Project = project;
            DirectoryPath = dir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            foreach (ClassStructure klass in Project.Classes)
            {
                Classes.Add(new ClassGenerator(klass));
            }
        }

        public void Generate()
        {
            foreach (ClassGenerator gen in Classes)
            {
                string file = DirectoryPath + "\\" + gen.ClassStructure.Name + ".cs";
                gen.InsertLines(file);
            }
        }
    }
}
