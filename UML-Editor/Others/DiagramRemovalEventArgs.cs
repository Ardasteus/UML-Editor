using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.Others
{
    public class DiagramRemovalEventArgs : EventArgs
    {
        public UMLDiagram Diagram;
        public DiagramRemovalEventArgs(UMLDiagram diagram)
        {
            Diagram = diagram;
        }
    }
}
