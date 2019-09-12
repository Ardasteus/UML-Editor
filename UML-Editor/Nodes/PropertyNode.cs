using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;

namespace UML_Editor.Nodes
{
    public class PropertyNode : FeatureNode
    {
        private ButtonNode AccessModifierButton;
        private TextBoxNode TypeTextBox;
        private TextBoxNode NameTextBox;
        private ContextMenuNode AccessModifiersContextMenuNode;

        public PropertyNode(string Type, string Name, AccessModifiers access_modifier, Modifiers modifier)
        {
             
        }
        public override void ForceResize(int width)
        {
        }

        public override List<INode> GetChildren()
        {
            return null;
        }

        public override void Render(Renderer renderer)
        {
        }
    }
}
