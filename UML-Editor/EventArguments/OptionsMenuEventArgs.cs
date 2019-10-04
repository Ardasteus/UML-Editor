using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;

namespace UML_Editor.EventArguments
{
    public class OptionsMenuEventArgs : EventArgs
    {
        public IOptionsNode Node { get; private set; }

        public OptionsMenuEventArgs(IOptionsNode optionsNode)
        {
            Node = optionsNode;
        }
    }
}
