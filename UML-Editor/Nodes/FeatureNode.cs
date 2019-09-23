using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Others;
using System.Drawing;

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
        public FeatureNode(string name, AccessModifiers accessModifier, Modifiers modifier)
        {
            Name = name;
            AccessModifier = accessModifier;
            Modifier = modifier;
        }

        public string Name { get; set; }
        public abstract Vector Position { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }
        public bool IsMenuShown { get; set; } = false;

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
            MenuPrefab = new ContextMenuNode("cnt", AccessModifierButton.Position + new Vector(AccessModifierButton.Width, 0), biggest, 0, RectangleRenderElementStyle.Default);
            MenuPrefab.AddNode(new ButtonNode("btn1", "Public", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Public;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                TriggerAreas.RemoveAt(1);
                IsMenuShown = false;
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Private", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Private;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                TriggerAreas.RemoveAt(1);
                IsMenuShown = false;
                GeneratePrefab();

            },
            RectangleRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode("btn1", "Protected", Vector.Zero, MenuPrefab.Width, Renderer.SingleTextHeight, () =>
            {
                AccessModifier = AccessModifiers.Protected;
                AccessModifierButton.Text = GetModifierChar();
                Height = Renderer.SingleTextHeight;
                AccessModifiersContextMenu = null;
                TriggerAreas.RemoveAt(1);
                IsMenuShown = false;
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));

        }

        public abstract int GetWidth();
        public void ShowMenu()
        {
            if (AccessModifiersContextMenu == null)
            {
                TriggerAreas.Add(new RectangleHitbox(MenuPrefab.Position, MenuPrefab.Width, MenuPrefab.Height));
                AccessModifiersContextMenu = MenuPrefab;
                OnFocused?.Invoke(this, new EventArgs());
            }
            else
            {
                TriggerAreas.RemoveAt(1);
                AccessModifiersContextMenu = null;
                OnUnfocused?.Invoke(this, new EventArgs());
            }
        }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
    }
}
