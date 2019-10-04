using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.EventArguments;

namespace UML_Editor.Nodes
{
    public interface ITextNode : INode
    {
        string Text { get; set; }
        EventHandler<TextEventArgs> OnTextChange { get; set; }
    }
}
