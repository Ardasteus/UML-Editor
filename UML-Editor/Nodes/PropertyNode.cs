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

namespace UML_Editor.Nodes
{
    public class PropertyNode : FeatureNode
    {
        private ButtonNode AccessModifierButton;
        private TextBoxNode TypeTextBox;
        private TextBoxNode NameTextBox;
        private ContextMenuNode AccessModifiersContextMenu;
        private ContextMenuNode MenuPrefab;
        private RectangleRenderElement BorderElement;
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

        public PropertyNode(string name, Vector position, string type, string prop_name, AccessModifiers access_modifier, Modifiers modifier) : base(name, position, access_modifier, modifier)
        {
            AccessModifierButton = new ButtonNode("accs_btn", GetModifierChar(), position, Renderer.SingleTextWidth, Renderer.SingleTextHeight, ShowMenu, RectangleRenderElementStyle.Default);
            NameTextBox = new TextBoxNode("type_txt", prop_name, new Vector(Renderer.SingleTextWidth, 0), Renderer.GetTextWidth(prop_name.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            TypeTextBox = new TextBoxNode("type_txt", type, new Vector(NameTextBox.Width + AccessModifierButton.Width, 0), Renderer.GetTextWidth(type.Length), Renderer.SingleTextHeight, false, Color.Black, Color.Black, Color.White);
            GeneratePrefab();
            Height = Renderer.SingleTextHeight;
            Width = GetWidth();
            BorderElement = new RectangleRenderElement(position, Width, Height, Color.White, Color.Black);
        }

        public int GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + TypeTextBox.Width;
        }

        public override void ForceResize(int width)
        {
        }

        private void GeneratePrefab()
        {
            int biggest = Renderer.GetTextWidth("Protected".Length);
            MenuPrefab = new ContextMenuNode("cnt", new Vector(AccessModifierButton.Position.X + Renderer.SingleTextWidth, 0), biggest, 0, RectangleRenderElementStyle.Default);
            MenuPrefab.AddNode(new ButtonNode("btn1", "Public", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Public;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Private", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Private;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Protected", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Protected;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
            },
            RectangleRenderElementStyle.Default));

        }

        private void ShowMenu()
        {
            if (AccessModifiersContextMenu == null)
            {
                Height = 3 *  Renderer.SingleTextHeight;
                AccessModifiersContextMenu = MenuPrefab;

            }
            else
                AccessModifiersContextMenu = null;
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
