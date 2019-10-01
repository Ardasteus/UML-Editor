using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Geometry;
using UML_Editor.Hitboxes;
using UML_Editor.NodeStructure;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class MethodNode : PropertyNode
    {
        public new MethodStructure CodeStructure { get; set; }

        public TextBoxNode ArgumentsTextBox { get; set; }
        public LabelNode LeftBracket { get; set; }
        public LabelNode RightBracket { get; set; }

        public MethodNode(MethodStructure codeStructure, BasicNodeStructure structure, RectangleRenderElementStyle border_style)
            : base(new PropertyStructure(codeStructure.Position, codeStructure.Name, codeStructure.Type, codeStructure.AccessModifier, codeStructure.Modifier), structure, border_style)
        {
            CodeStructure = codeStructure;
            LeftBracket = new LabelNode(new BasicTextNodeStructure(Position + new Vector(AccessModifierButton.Width + NameTextBox.Width, 0),
                Renderer.SingleTextWidth, Height, "("),
                TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            string argum = codeStructure.Arguments;
            ArgumentsTextBox = new TextBoxNode(new BasicTextNodeStructure(Position + new Vector(AccessModifierButton.Width + NameTextBox.Width + LeftBracket.Width, 0),
                Renderer.GetTextWidth(argum.Length), Height, argum),
                TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            RightBracket = new LabelNode(new BasicTextNodeStructure(Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width, 0),
                    Renderer.SingleTextWidth, Height, ")"),
                TextRenderElementStyle.Default, RectangleRenderElementStyle.Textbox);
            Children.Add(LeftBracket);
            Children.Add(RightBracket);
            Children.Add(ArgumentsTextBox);
            ArgumentsTextBox.OnFocused += HideMenu;
            ArgumentsTextBox.OnFocused += HideOptions;
            SetEvents();
        }

        public override float GetWidth()
        {
            return AccessModifierButton.Width + NameTextBox.Width + Separator.Width + TypeTextBox.Width + LeftBracket.Width + RightBracket.Width + ArgumentsTextBox.Width;
        }

        public override void RepositionChildren()
        {
            float new_width = GetWidth();
            if (Width < new_width)
                Width = new_width;
            AccessModifierButton.Position = new Vector(Position.X, Position.Y);
            NameTextBox.Position = Position + new Vector(AccessModifierButton.Width, 0);
            ArgumentsTextBox.Position = Position + new Vector(AccessModifierButton.Width + NameTextBox.Width + LeftBracket.Width, 0);
            TypeTextBox.Position = new Vector(Position.X + AccessModifierButton.Width + NameTextBox.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width + Separator.Width, Position.Y);
            LeftBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width, 0);
            RightBracket.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width, 0);
            Separator.Position = Position + new Vector(NameTextBox.Width + AccessModifierButton.Width + ArgumentsTextBox.Width + LeftBracket.Width + RightBracket.Width, 0);
        }
    }
}
