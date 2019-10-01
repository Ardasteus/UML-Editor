using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class MethodNode : FeatureNode
    {
        public MethodStructure Structure;
        public override string Name
        {
            get => NameTextBox.Text;
            set
            {
                NameTextBox.Text = value;
                Structure.Name = value;
            }
        }
        public override string Type
        {
            get => TypeTextBox.Text;
            set
            {
                TypeTextBox.Text = value;
                Structure.Type = value;
            }
        }
        public TextBoxNode ArgumentsTextBox { get; set; }
        public LabelNode LeftBracket { get; set; }
        public LabelNode RightBracket { get; set; }
        public MethodNode(MethodStructure structure) : base()
        {
            Vector position = structure.Position;
            Structure = structure;
            AccessModifier = Structure.AccessModifier;
            Modifier = Structure.Modifier;
            AccessModifierButton = new ButtonNode("accs_btn", GetModifierChar(), position, Renderer.SingleTextWidth, Renderer.SingleTextHeight, ShowMenu, new RectangleRenderElementStyle(Color.White, Color.White, 1));
            NameTextBox = new TextBoxNode("name_txt", structure.Name, position + new Vector(Renderer.SingleTextWidth, 0), Renderer.GetTextWidth(structure.Name.Length), Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            LeftBracket = new LabelNode("rbrkt", "(", position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0));
            ArgumentsTextBox = new TextBoxNode("args_txt", "type name", position + new Vector(NameTextBox.Width + AccessModifierButton.Width + LeftBracket.Width, 0), Renderer.GetTextWidth(4), Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            RightBracket = new LabelNode("lbrkt", ")", position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width, 0));
            Separator = new LabelNode("separator", ":", position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width, 0));
            TypeTextBox = new TextBoxNode("type_txt", structure.Type, position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width + Separator.Width, 0), Renderer.GetTextWidth(structure.Type.Length), Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            BorderElement = new RectangleRenderElement(position, GetWidth(), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Width, Height));
            NameTextBox.OnResize = Resize;
            TypeTextBox.OnResize = Resize;
            ArgumentsTextBox.OnResize = Resize;
            GeneratePrefab();
            GenerateOptionsMenu();
        }
        public override Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
                AccessModifierButton.Position = value;
                NameTextBox.Position = Position + new Vector(AccessModifierButton.Width, 0);
                ArgumentsTextBox.Position = Position + new Vector(AccessModifierButton.Width + NameTextBox.Width + LeftBracket.Width, 0);
                TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width + Separator.Width, Position.Y);
                LeftBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0);
                RightBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width, 0);
                Separator.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width, 0);
                GeneratePrefab();
            }
        }
        public override int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
                OnResize?.Invoke(this, new ResizeEventArgs(Width));
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
        private void Resize(object sender, ResizeEventArgs args)
        {
            Width = GetWidth();
            NameTextBox.Position = Position + new Vector(AccessModifierButton.Width, 0);
            ArgumentsTextBox.Position = Position + new Vector(AccessModifierButton.Width + NameTextBox.Width + LeftBracket.Width, 0);
            TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width + Separator.Width, Position.Y);
            LeftBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0);
            RightBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width, 0);
            Separator.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width, 0);
        }

        public override List<INode> GetChildren()
        {
            List<INode> ret = new List<INode>();
            if (AccessModifiersContextMenu != null)
                ret.Add(AccessModifiersContextMenu);
            if (OptionsMenu != null)
                ret.Add(OptionsMenu);
            ret.Add(AccessModifierButton);
            ret.Add(TypeTextBox);
            ret.Add(NameTextBox);
            ret.Add(ArgumentsTextBox);
            ret.Add(LeftBracket);
            ret.Add(RightBracket);
            ret.Add(Separator);
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            AccessModifierButton.Render(renderer);
            NameTextBox.Render(renderer);
            TypeTextBox.Render(renderer);
            Separator.Render(renderer);
            LeftBracket.Render(renderer);
            RightBracket.Render(renderer);
            ArgumentsTextBox.Render(renderer);
            if (AccessModifiersContextMenu != null)
                AccessModifiersContextMenu.Render(renderer);
            if (OptionsMenu != null)
                OptionsMenu.Render(renderer);
        }

        public override int GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + TypeTextBox.Width + ArgumentsTextBox.Width + RightBracket.Width + Separator.Width + LeftBracket.Width;

        }
    }
}
