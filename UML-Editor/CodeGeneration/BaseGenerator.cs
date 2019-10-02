using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.CodeGeneration
{
    public abstract class BaseGenerator
    {
        public virtual List<string> Lines { get; set; } = new List<string>();

        public virtual void InsertLines(string file)
        {
            using (StreamWriter sw = File.AppendText(file))
            {
                foreach (string line in Lines)
                {
                    sw.WriteLine(line);
                }
                sw.WriteLine();
            }
        }
    }
}
