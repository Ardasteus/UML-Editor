using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;
using UML_Editor.Rendering.ElementStyles;
using System.Drawing;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Others;

namespace UML_Editor.Nodes
{
    public class PropertyNode : FeatureNode
    {
        public string PropertyName
        {
            get => NameTextBox.Text;
            set => NameTextBox.Text = value;
        }
        public string PropertyType
        {
            get => TypeTextBox.Text;
            set => TypeTextBox.Text = value;
        }

        public PropertyNode(string name, Vector position, string type, string prop_name, AccessModifiers access_modifier, Modifiers modifier) : base(name, access_modifier, modifier)
        {
            AccessModifierButton = new ButtonNode("accs_btn", GetModifierChar(), position, Renderer.SingleTextWidth, Renderer.SingleTextHeight, ShowMenu, RectangleRenderElementStyle.Default);
            NameTextBox = new TextBoxNode("type_txt", prop_name, position + new Vector(Renderer.SingleTextWidth, 0), Renderer.GetTextWidth(prop_name.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            TypeTextBox = new TextBoxNode("type_txt", type, position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0), Renderer.GetTextWidth(type.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            BorderElement = new RectangleRenderElement(position, GetWidth(), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Width, Height));
            GeneratePrefab();
        }

        public override Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
            }
        }
        public override int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
            }
        }
        public override int Height
        {
            get => BorderElement.Height;
            set
            {
                BorderElement.Height = value;
                ((RectangleHitbox)TriggerAreas[0]).Height = value;
            }
        }

        public override int GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + TypeTextBox.Width;
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
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            AccessModifierButton.Render(renderer);
            NameTextBox.Render(renderer);
            TypeTextBox.Render(renderer);
            if (AccessModifiersContextMenu != null)
                AccessModifiersContextMenu.Render(renderer);
        }
    }
}
