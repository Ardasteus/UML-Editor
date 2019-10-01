using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.EventArguments;
using UML_Editor.Hitboxes;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class ClassDiagramNode : BasicContainerNode, IOptionsNode, IFocusableNode
    {
        public ClassStructure CodeStructure { get; set; }
        public List<PropertyNode> Properties = new List<PropertyNode>();
        public List<MethodNode> Methods = new List<MethodNode>();
        public BasicContainerNode OptionsPrefab { get; set; }
        public BasicContainerNode OptionsMenu { get; set; }
        private TextBoxNode NameTextBox;
        private LineRenderElement NameLine;
        private LineRenderElement SeparatorLine;

        public ClassDiagramNode(ClassStructure codestructure, BasicNodeStructure structure, RectangleRenderElementStyle border_style) : base(structure, border_style)
        {
            CodeStructure = codestructure;
            NameTextBox = new TextBoxNode(new BasicTextNodeStructure(Position, Renderer.GetTextWidth(Name.Length), Height, Name), TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            NameLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            SeparatorLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            Children.Add(NameTextBox);
            GeneratePrefab();
            RepositionChildren();
            SetEvents();
        }
        public void SetEvents()
        {
            OnOptionsShow += ShowOptions;
            OnOptionsHide += HideOptions;
            Children.OfType<IFocusableNode>().ToList().ForEach(x =>
            {
                x.OnFocused += HideOptions;
                x.OnFocused += OnNodeFocus;
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

        public override void AddNode(INode node)
        {
            if (node is MethodNode mn)
            {
                Methods.Add(mn);
                mn.OnHitboxCreation += AddHitbox;
                mn.OnHitboxDeletion += RemoveHitbox;
                CodeStructure.Methods.Add(mn.CodeStructure);
                Height += Renderer.SingleTextHeight;
                mn.RepositionChildren();
                base.AddNode(node);
            }
            else if (node is PropertyNode pn)
            {
                Properties.Add(pn);
                pn.OnHitboxCreation += AddHitbox;
                pn.OnHitboxDeletion += RemoveHitbox;
                CodeStructure.Properties.Add(pn.CodeStructure);
                Height += Renderer.SingleTextHeight;
                pn.RepositionChildren();
                base.AddNode(node);
            }
        }

        public virtual AccessModifiers AccessModifier
        {
            get => CodeStructure.AccessModifier;
            set
            {
                CodeStructure.AccessModifier = value;
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
                OnCodeStructureChange?.Invoke(this, new CodeStructureEventArgs(CodeStructure));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }
        public float GetWidest()
        {
            INode temp_node = Children.OrderByDescending(x => x.Width).FirstOrDefault();
            return temp_node.Width;
        }
        public override void RepositionChildren()
        {
            float new_width = GetWidest();
            if (Width < new_width)
                Width = new_width;
            NameTextBox.Position = new Vector((Position.X + Width / 2) - (NameTextBox.Width / 2), Position.Y);
            for (int i = 0; i < Properties.Count; i++)
            {
                Properties[i].Position = Position + new Vector(0, (i + 1) * Renderer.SingleTextHeight);
            }
            for (int i = 0; i < Methods.Count; i++)
            {
                Methods[i].Position = Position + new Vector(0, (i + Properties.Count + 1) * Renderer.SingleTextHeight);
            }
            NameLine.StartPoint = new Vector(Position.X, Position.Y + Renderer.SingleTextHeight);
            NameLine.EndPoint = new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight);
            if (Properties.Count > 0)
            {
                PropertyNode prop = Properties.Last();
                SeparatorLine.StartPoint = prop.Position + new Vector(0, Renderer.SingleTextHeight);
                SeparatorLine.EndPoint = prop.Position + new Vector(Width, Renderer.SingleTextHeight);
            }
            else
            {
                SeparatorLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            }
        }
        public override void OnNodeFocus(object sender, NodeEventArgs e)
        {
            if (FocusedNode != e.Node)
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
        }
        public void GeneratePrefab()
        {
            float total_Width = Renderer.GetTextWidth(13);
            OptionsPrefab = new BasicContainerNode(new BasicNodeStructure(Vector.Zero, total_Width, Renderer.SingleTextHeight * 3), RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Make Regular", total_Width, Renderer.SingleTextHeight, () =>
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
        public EventHandler OnOptionsShow { get; set; }
        public EventHandler OnOptionsHide { get; set; }
        public EventHandler<NodeEventArgs> OnFocused { get; set; }
        public EventHandler<NodeEventArgs> OnUnfocused { get; set; }
        public EventHandler OnMouseClick { get; set; }
        public EventHandler<CodeStructureEventArgs> OnCodeStructureChange { get; set; }
        public void AddHitbox(object sender, HitboxEventArgs e)
        {
            TriggerAreas.Add(e.Hitbox);
        }

        public void RemoveHitbox(object sender, HitboxEventArgs e)
        {
            TriggerAreas.Remove(e.Hitbox);
        }
        public void ShowOptions(object sender, EventArgs e)
        {
            if (OptionsMenu == null)
            {
                OptionsMenu = OptionsPrefab;
                TriggerAreas.Add(OptionsMenu.TriggerAreas[0]);
                Children.Add(OptionsMenu);
                OnFocused?.Invoke(this, new NodeEventArgs(this));
            }
            else
                OnOptionsHide?.Invoke(this, e);
        }
        public void HideOptions(object sender, EventArgs e)
        {
            if (OptionsMenu != null)
            {
                Children.Remove(OptionsMenu);
                TriggerAreas.Remove(OptionsMenu.TriggerAreas[0]);
            }
            OptionsMenu = null;
        }

        public override void Render(Renderer renderer)
        {
            base.Render(renderer);
            SeparatorLine.Render(renderer);
            NameLine.Render(renderer);
            BorderElement.BorderOnly(renderer);
            ((PropertyNode)FocusedNode)?.FocusedNode?.Render(renderer);
            ((PropertyNode)FocusedNode)?.OptionsMenu?.Render(renderer);
            ((PropertyNode)FocusedNode)?.AccessModifierMenu?.Render(renderer);
        }

        public bool IsOnEdge(Vector v)
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            return v.X == left || v.X == right || v.Y == top || v.Y == bot;
        }

        public List<Vector> GetSideCenters()
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            List<Vector> centers = new List<Vector>();
            centers.Add(new Vector((left + right) / 2, top));
            centers.Add(new Vector(right, (top + bot) / 2));
            centers.Add(new Vector((left + right) / 2, bot));
            centers.Add(new Vector(left, (top + bot) / 2));
            return centers;
        }

        public Vector GetTopAnchor() => new Vector((Position.X + (Width / 2)), Position.Y);
        public Vector GetBotAnchor() => new Vector((Position.X + (Width / 2)), Position.Y + Height);
        public Vector GetLeftAnchor() => new Vector(Position.X, Position.Y + (Height / 2));
        public Vector GetRightAnchor() => new Vector(Position.X + Width, Position.Y + (Height / 2));
        public Vector GetCenter() => new Vector(Position.X + (Width / 2), Position.Y + (Height / 2));
        public Vector GetTopLeftCorner() => new Vector(Position.X, Position.Y);
        public Vector GetTopRightCorner() => new Vector(Position.X + Width, Position.Y);
        public Vector GetBotLeftCorner() => new Vector(Position.X, Position.Y + Height);
        public Vector GetBotRightCorner() => new Vector(Position.X + Width, Position.Y + Height);
        public Line GetTopSide() => new Line(GetTopLeftCorner(), GetTopRightCorner());
        public Line GetBotSide() => new Line(GetBotLeftCorner(), GetBotRightCorner());
        public Line GetLeftSide() => new Line(GetTopLeftCorner(), GetBotLeftCorner());
        public Line GetRightSide() => new Line(GetTopRightCorner(), GetBotRightCorner());

    }
}
