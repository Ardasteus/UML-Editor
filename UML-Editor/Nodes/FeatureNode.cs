using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Nodes
{
    public abstract class FeatureNode : IRenderableNode, IContainerNode
    {
        public ContextMenuNode AccessModifiersContextMenu { get; set; }
        public ContextMenuNode MenuPrefab { get; set; }
        public ButtonNode AccessModifierButton { get; set; }
        public TextBoxNode TypeTextBox { get; set; }
        public TextBoxNode NameTextBox { get; set; }
        public RectangleRenderElement BorderElement { get; set; }
        public FeatureNode(string name, Vector position, AccessModifiers accessModifier, Modifiers modifier)
        {
            Name = name;
            Position = position;
            AccessModifier = accessModifier;
            Modifier = modifier;
        }

        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Resize { get; set; }
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }

        public abstract void ForceResize(int width);

        public string GetModifierChar()
        {
            switch (AccessModifier)
            {
                case AccessModifiers.Private:
                    return "-";
                case AccessModifiers.Public:
                    return "+";
                case AccessModifiers.Protected:
                    return "#";
                default:
                    return "E";
            }
        }

        public abstract List<INode> GetChildren();

        public abstract void Render(Renderer renderer);
        public void GeneratePrefab()
        {
            int biggest = Renderer.GetTextWidth("Protected".Length);
            MenuPrefab = new ContextMenuNode("cnt", new Vector(AccessModifierButton.Position.X + Renderer.SingleTextWidth, 0), biggest, 0, RectangleRenderElementStyle.Default);
            MenuPrefab.AddNode(new ButtonNode("btn1", "Public", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Public;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                GeneratePrefab();

            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Private", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Private;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                GeneratePrefab();

            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Protected", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Protected;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));

        }

        public abstract int GetWidth();
        public void ShowMenu()
        {
            if (AccessModifiersContextMenu == null)
            {
                Height = 3 * Renderer.SingleTextHeight;
                AccessModifiersContextMenu = MenuPrefab;
            }
            else
                AccessModifiersContextMenu = null;
        }
    }
}
