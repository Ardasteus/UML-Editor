using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Hitboxes;
using System.Windows.Forms;

namespace UML_Editor.Nodes
{
    public interface IMouseFocusableNode : IFocusableNode
    {
        EventHandler OnMouseClick { get; set; }
    }
}
