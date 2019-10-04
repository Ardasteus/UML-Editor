using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor
{
    public class EditorChangeEventArgs
    {
        public int Index;
        public EditorChangeEventArgs(int index)
        {
            Index = index;
        }
    }
}
