using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Hitboxes;
using System.Drawing;
using System.Windows.Forms;
using UML_Editor.EventArguments;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class PropertyNode : BasicContainerNode, IOptionsNode
    {
        public BasicContainerNode AccessModifierMenu { get; set; }
        public BasicContainerNode MenuPrefab { get; set; }
        public ButtonNode AccessModifierButton { get; set; }
        public TextBoxNode TypeTextBox { get; set; }
        public TextBoxNode NameTextBox { get; set; }
        public LabelNode Separator { get; set; }

        public PropertyStructure CodeStructure { get; set; }

        public PropertyNode(PropertyStructure codeStructure, BasicNodeStructure structure, RectangleRenderElementStyle border_style) : base(structure, border_style)
        {
            CodeStructure = codeStructure;
            AccessModifierButton = new ButtonNode(new ButtonStructure(Position, "+", Renderer.SingleTextWidth, Height, () => OnMenuShow?.Invoke(this, EventArgs.Empty)), RectangleRenderElementStyle.Textbox, TextRenderElementStyle.Default);
            NameTextBox = new TextBoxNode( new BasicTextNodeStructure(Position, Renderer.GetTextWidth(Name.Length), Height, Name), TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            Separator = new LabelNode(new BasicTextNodeStructure(Position, Renderer.SingleTextWidth, Height, ":"), TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            TypeTextBox = new TextBoxNode(new BasicTextNodeStructure(Position, Renderer.GetTextWidth(Type.Length), Height, Type), TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            Children.Add(AccessModifierButton);
            Children.Add(NameTextBox);
            Children.Add(Separator);
            Children.Add(TypeTextBox);
            OnUnfocused += OnUnFocus;
            GenerateMenu();
            GenerateOptions();
        }

        public void SetEvents()
        {
            OnMenuHide += HideMenu;
            OnMenuShow += ShowMenu;
            OnOptionsShow += ShowOptions;
            OnOptionsHide += HideOptions;
            OnOptionsShow += HideMenu;
            OnMenuShow += HideOptions;
            OnChange += HideMenu;
            OnChange += HideOptions;
            Children.ForEach(x => x.OnResize += OnChildResize);
            Children.OfType<IFocusableNode>().ToList().ForEach(x =>
            {
                if(!(x is ButtonNode))
                {
                    x.OnFocused += HideMenu;
                    x.OnFocused += HideOptions;
                    x.OnFocused += OnNodeFocus;
                }
                x.OnUnfocused += OnNodeUnfocus;
            });
        }
        public virtual string Name
        {
            get => CodeStructure.Name;
            set
            {
                CodeStructure.Name = value;
                NameTextBox.Text = value;
                OnCodeStructureChange?.Invoke(this, new CodeStructureEventArgs(CodeStructure));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }
        public virtual string Type
        {
            get => CodeStructure.Type;
            set
            {
                CodeStructure.Type = value;
                TypeTextBox.Text = value;
                OnCodeStructureChange?.Invoke(this, new CodeStructureEventArgs(CodeStructure));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual AccessModifiers AccessModifier
        {
            get => CodeStructure.AccessModifier;
            set
            {
                CodeStructure.AccessModifier = value;
                SetAccessModifier();
                OnCodeStructureChange?.Invoke(this, new CodeStructureEventArgs(CodeStructure));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }
        public virtual Modifiers Modifier
        {
            get => CodeStructure.Modifier;
            set
            {
                CodeStructure.Modifier = value;
                SetModifier();
                OnCodeStructureChange?.Invoke(this, new CodeStructureEventArgs(CodeStructure));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float GetWidth() => AccessModifierButton.Width + NameTextBox.Width + Separator.Width + TypeTextBox.Width;

        public void OnUnFocus(object sender, NodeEventArgs e)
        {
            FocusedNode?.OnUnfocused?.Invoke(this, new NodeEventArgs(FocusedNode));
            OnMenuHide?.Invoke(this, EventArgs.Empty);
            OnOptionsHide?.Invoke(this, EventArgs.Empty);
        }
        public override void OnNodeFocus(object sender, NodeEventArgs e)
        {
            if (FocusedNode != e.Node && !(e.Node is ButtonNode))
            {
                OnFocused?.Invoke(this, new NodeEventArgs(this));
                FocusedNode?.OnUnfocused?.Invoke(this, new NodeEventArgs(FocusedNode));
                FocusedNode = (IFocusableNode)e.Node;
            }
        }
        public override void OnNodeUnfocus(object sender, NodeEventArgs e)
        {
            if (FocusedNode == e.Node)
                FocusedNode = null;
            if (AccessModifierMenu == null && OptionsMenu == null)
                OnUnfocused?.Invoke(this, new NodeEventArgs(this));
        }
        public override void RepositionChildren()
        {
            float new_width = GetWidth();
            if (Width < new_width)
                Width = new_width;
            if (Height != Renderer.SingleTextHeight)
                Height = Renderer.SingleTextHeight;
            AccessModifierButton.Position = new Vector(Position.X, Position.Y);
            NameTextBox.Position = new Vector(Position.X + AccessModifierButton.Width, Position.Y);
            Separator.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width, Position.Y);
            TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + Separator.Width, Position.Y);
        }

        public override void Render(Renderer renderer)
        {
            base.Render(renderer);
            OptionsMenu?.Render(renderer);
            AccessModifierMenu?.Render(renderer);
        }

        public virtual void SetAccessModifier()
        {
            switch (AccessModifier)
            {
                case AccessModifiers.Private:
                    AccessModifierButton.Text = "-";
                    break;
                case AccessModifiers.Public:
                    AccessModifierButton.Text = "+";
                    break;
                case AccessModifiers.Protected:
                    AccessModifierButton.Text = "#";
                    break;
                default:
                    AccessModifierButton.Text = "E";
                    break;
            }
        }

        public virtual void SetModifier()
        {
            switch (Modifier)
            {
                case Modifiers.None:
                    NameTextBox.Style = FontStyle.Regular;
                    break;
                case Modifiers.Static:
                    NameTextBox.Style = FontStyle.Underline;
                    break;
                case Modifiers.Abstract:
                    NameTextBox.Style = FontStyle.Italic;
                    break;
            }
        }

        public EventHandler<NodeEventArgs> OnFocused { get; set; }
        public EventHandler<NodeEventArgs> OnUnfocused { get; set; }
        public EventHandler<CodeStructureEventArgs> OnCodeStructureChange { get; set; }
        public EventHandler OnMouseClick { get; set; }
        public BasicContainerNode OptionsPrefab { get; set; }
        public BasicContainerNode OptionsMenu { get; set; }
        public virtual void GenerateOptions()
        {
            float total_Width = Renderer.GetTextWidth(13);
            OptionsPrefab = new BasicContainerNode(new BasicNodeStructure(Vector.Zero, total_Width, Renderer.SingleTextHeight * 3), RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Regular", total_Width, Renderer.SingleTextHeight , () =>
                {
                    Modifier = Modifiers.None;
                    OnOptionsHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Abstract", total_Width, Renderer.SingleTextHeight, () =>
                {
                    Modifier = Modifiers.Abstract;
                    OnOptionsHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Static", total_Width, Renderer.SingleTextHeight, () =>
                {
                    Modifier = Modifiers.Static;
                    OnOptionsHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
        }

        public virtual void GenerateMenu()
        {
            float total_Width = Renderer.GetTextWidth(9);
            MenuPrefab = new BasicContainerNode(new BasicNodeStructure(Vector.Zero, total_Width, Renderer.SingleTextHeight * 3), RectangleRenderElementStyle.Default);
            MenuPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Public", total_Width, Renderer.SingleTextHeight, () =>
                {
                    AccessModifier = AccessModifiers.Public;
                    OnMenuHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Private", total_Width, Renderer.SingleTextHeight, () =>
                {
                    AccessModifier = AccessModifiers.Private;
                    OnMenuHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
            MenuPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Protected", total_Width, Renderer.SingleTextHeight, () =>
                {
                    AccessModifier = AccessModifiers.Protected;
                    OnMenuHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
        }

        public void AddHitbox(IHitbox hitbox)
        {
            TriggerAreas.Add(hitbox);
            OnHitboxCreation?.Invoke(this, new HitboxEventArgs(hitbox));
        }

        public void RemoveHitbox(IHitbox hitbox)
        {
            TriggerAreas.Remove(hitbox);
            OnHitboxDeletion?.Invoke(this, new HitboxEventArgs(hitbox));
        }
        public void ShowMenu(object sender, EventArgs e)
        {
            if (AccessModifierMenu == null)
            {
                MenuPrefab.Position = AccessModifierButton.Position + new Vector(AccessModifierButton.Width, 0);
                AccessModifierMenu = MenuPrefab;
                PrependNode(AccessModifierMenu);
                OnFocused?.Invoke(this, new NodeEventArgs(this));
                AddHitbox(AccessModifierMenu.TriggerAreas[0]);
            }
        }
        public void HideMenu(object sender, EventArgs e)
        {
            if (AccessModifierMenu != null)
            {
                RemoveHitbox(AccessModifierMenu.TriggerAreas[0]);
                Children.Remove(AccessModifierMenu);
            }
            AccessModifierMenu = null;
        }
        public void ShowOptions(object sender, EventArgs e)
        {
            if (OptionsMenu == null)
            {
                OptionsMenu = OptionsPrefab;
                AddHitbox(OptionsMenu.TriggerAreas[0]);
                PrependNode(OptionsMenu);
                OnFocused?.Invoke(this, new NodeEventArgs(this));
                AddHitbox(OptionsMenu.TriggerAreas[0]);
            }
            else
                OnOptionsHide?.Invoke(this, e);
        }
        public void HideOptions(object sender, EventArgs e)
        {
            if(OptionsMenu != null)
            {
                RemoveHitbox(OptionsMenu.TriggerAreas[0]);
                Children.Remove(OptionsMenu);
            }
            OptionsMenu = null;
        }
        public EventHandler OnOptionsShow { get; set; }
        public EventHandler OnOptionsHide { get; set; }
        public EventHandler OnMenuShow { get; set; }
        public EventHandler OnMenuHide { get; set; }
        public EventHandler<HitboxEventArgs> OnHitboxCreation { get; set; }
        public EventHandler<HitboxEventArgs> OnHitboxDeletion { get; set; }
    }
}
