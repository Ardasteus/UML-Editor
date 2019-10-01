using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UML_Editor.Nodes
{
    public interface IKeyboardFocusableNode : IFocusableNode
    {
        EventHandler<KeyPressEventArgs> OnKeyPress { get; set; }
    }
}
