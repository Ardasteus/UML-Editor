using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class MethodNode : FeatureNode
    {
        public TextBoxNode ArgumentsTextBox { get; set; }
        public MethodNode(string name, Vector position, string type, string prop_name, AccessModifiers access_modifier, Modifiers modifier) : base(name, position, access_modifier, modifier)
        {
            AccessModifierButton = new ButtonNode("accs_btn", GetModifierChar(), position, Renderer.SingleTextWidth, Renderer.SingleTextHeight, ShowMenu, RectangleRenderElementStyle.Default);
            NameTextBox = new TextBoxNode("name_txt", prop_name, position + new Vector(Renderer.SingleTextWidth, 0), Renderer.GetTextWidth(prop_name.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            ArgumentsTextBox = new TextBoxNode("args_txt", "args", position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0), Renderer.GetTextWidth(4), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            TypeTextBox = new TextBoxNode("type_txt", type, position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width, 0), Renderer.GetTextWidth(type.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            GeneratePrefab();
            Height = Renderer.SingleTextHeight;
            Width = GetWidth();
            BorderElement = new RectangleRenderElement(position, Width, Height, Color.White, Color.Black);
        }

        public override void ForceResize(int width)
        {
        }

        public override List<INode> GetChildren()
        {
            List<INode> ret = new List<INode>();
            if (AccessModifiersContextMenu != null)
                ret.Add(AccessModifiersContextMenu);
            ret.Add(AccessModifierButton);
            ret.Add(TypeTextBox);
            ret.Add(NameTextBox);
            ret.Add(ArgumentsTextBox);
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            AccessModifierButton.Render(renderer);
            NameTextBox.Render(renderer);
            TypeTextBox.Render(renderer);
            ArgumentsTextBox.Render(renderer);
            if (AccessModifiersContextMenu != null)
                AccessModifiersContextMenu.Render(renderer);
        }

        public override int GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + TypeTextBox.Width + ArgumentsTextBox.Width;

        }
    }
}
