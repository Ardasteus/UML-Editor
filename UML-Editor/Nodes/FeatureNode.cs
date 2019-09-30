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
using UML_Editor.Geometry;

namespace UML_Editor.Nodes
{
    public abstract class FeatureNode : IRenderableNode, IContainerNode, IOptionsNode
    {
        public ContextMenuNode AccessModifiersContextMenu { get; set; }
        public ContextMenuNode MenuPrefab { get; set; }
        public ButtonNode AccessModifierButton { get; set; }
        public TextBoxNode TypeTextBox { get; set; }
        public TextBoxNode NameTextBox { get; set; }
        public RectangleRenderElement BorderElement { get; set; }
        public LabelNode Separator { get; set; }

        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public abstract Vector Position { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        private AccessModifiers accessmodifier;
        public virtual AccessModifiers AccessModifier
        {
            get => accessmodifier;
            set
            {
                accessmodifier = value;
                if(AccessModifierButton != null)
                    AccessModifierButton.Text = GetModifierChar();
            }
        }
        private Modifiers modifier;
        public virtual Modifiers Modifier
        {
            get => modifier;
            set
            {
                modifier = value;
                if(NameTextBox != null)
                    switch (Modifier)
                    {
                        case Modifiers.None:
                            NameTextBox.TextStyle = FontStyle.Regular;
                            break;
                        case Modifiers.Static:
                            NameTextBox.TextStyle = FontStyle.Underline;
                            break;
                        case Modifiers.Abstract:
                            NameTextBox.TextStyle = FontStyle.Italic;
                            break;
                    }
            }
        }
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
                if(OptionsMenu != null)
                {
                    OnHitboxRemoval?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                    TriggerAreas.RemoveAt(1);
                    OptionsMenu = null;
                }
                TriggerAreas.Add(new RectangleHitbox(MenuPrefab.Position, MenuPrefab.Width, MenuPrefab.Height));
                OnHitboxCreate?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                AccessModifiersContextMenu = MenuPrefab;
                OnFocused?.Invoke(this, new EventArgs());
            }
            else
            {
                OnHitboxRemoval?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
                AccessModifiersContextMenu = null;
                OnUnfocused?.Invoke(this, new EventArgs());
            }
        }

        public void HandleMouse()
        {
        }

        public void ShowOptionsMenu()
        {
            if (OptionsMenu == null)
            {
                if (AccessModifiersContextMenu != null)
                {
                    OnHitboxRemoval?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                    TriggerAreas.RemoveAt(1);
                    AccessModifiersContextMenu = null;
                }
                TriggerAreas.Add(new RectangleHitbox(OptionsPrefab.Position, OptionsPrefab.Width, OptionsPrefab.Height));
                OnHitboxCreate?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                OptionsMenu = OptionsPrefab;
                OnFocused?.Invoke(this, new EventArgs());
            }
            else
            {
                OnHitboxRemoval?.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
                OptionsMenu = null;
                OnUnfocused?.Invoke(this, new EventArgs());
            }
        }

        public void GenerateOptionsMenu()
        {

            OptionsPrefab = new ContextMenuNode("cnt", Vector.Zero, 0, 0, RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Make Normal", Vector.Zero, Renderer.GetTextWidth(11), Renderer.SingleTextHeight, () =>
            {
                Modifier = Modifiers.None;
                OptionsMenu = null;
                OnHitboxRemoval.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Make Static", Vector.Zero, Renderer.GetTextWidth(11), Renderer.SingleTextHeight, () =>
            {
                Modifier = Modifiers.Static;
                OptionsMenu = null;
                OnHitboxRemoval.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Make Abstract", Vector.Zero, Renderer.GetTextWidth(14), Renderer.SingleTextHeight, () =>
            {
                Modifier = Modifiers.Abstract;
                OptionsMenu = null;
                OnHitboxRemoval.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Remove", Vector.Zero, Renderer.GetTextWidth(6), Renderer.SingleTextHeight, () =>
            {
                OnRemoval?.Invoke(this, new FeatureRemovalEventArgs(this));
                OptionsMenu = null;
                OnHitboxRemoval.Invoke(this, new HitboxEventArgs(TriggerAreas[1]));
                TriggerAreas.RemoveAt(1);
            },
            RectangleRenderElementStyle.Default));
        }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        public ContextMenuNode OptionsPrefab { get; set; }
        public ContextMenuNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }

        public EventHandler<HitboxEventArgs> OnHitboxCreate;
        public EventHandler<HitboxEventArgs> OnHitboxRemoval;
        public EventHandler<FeatureRemovalEventArgs> OnRemoval;
    }
}
