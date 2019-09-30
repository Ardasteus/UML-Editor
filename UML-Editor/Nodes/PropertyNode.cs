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
using UML_Editor.Geometry;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class PropertyNode : FeatureNode
    {
        public PropertyStructure Structure;

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

        public override Modifiers Modifier
        {
            get => base.Modifier;
            set
            {
                base.Modifier = value;
                Structure.Modifier = value;
            }
        }
        public override AccessModifiers AccessModifier
        {
            get => base.AccessModifier;
            set
            {
                base.AccessModifier = value;
                Structure.AccessModifier = value;
            }
        }

        public PropertyNode(Vector position, PropertyStructure structure) : base()
        {
            Structure = structure;
            AccessModifier = Structure.AccessModifier;
            Modifier = Structure.Modifier;
            AccessModifierButton = new ButtonNode("accs_btn", GetModifierChar(), position, Renderer.SingleTextWidth, Renderer.SingleTextHeight, ShowMenu, new RectangleRenderElementStyle(Color.White, Color.White, 1));
            NameTextBox = new TextBoxNode("type_txt", structure.Name, position + new Vector(Renderer.SingleTextWidth, 0), Renderer.GetTextWidth(structure.Name.Length), Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            Separator = new LabelNode("separator", ":", position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0));
            TypeTextBox = new TextBoxNode("type_txt", structure.Type, position + new Vector(NameTextBox.Width + AccessModifierButton.Width + Separator.Width, 0), Renderer.GetTextWidth(structure.Type.Length), Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            BorderElement = new RectangleRenderElement(position, GetWidth(), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Width, Height));
            NameTextBox.OnResize = Resize;
            TypeTextBox.OnResize = Resize;
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
                AccessModifierButton.Position = Position;
                NameTextBox.Position = new Vector(Position.X + AccessModifierButton.Width, Position.Y);
                Separator.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width, Position.Y);
                TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + Separator.Width, Position.Y);
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

        public override int GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + TypeTextBox.Width + Separator.Width;
        }

        private void Resize(object sender, ResizeEventArgs args)
        {
            Width = GetWidth();
            NameTextBox.Position = new Vector(Position.X + AccessModifierButton.Width, Position.Y);
            Separator.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width, Position.Y);
            TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + Separator.Width, Position.Y);
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
            if (AccessModifiersContextMenu != null)
                AccessModifiersContextMenu.Render(renderer);
            if (OptionsMenu != null)
                OptionsMenu.Render(renderer);
        }
    }
}
