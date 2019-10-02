using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UML_Editor.EventArguments;
using UML_Editor.Geometry;
using UML_Editor.Hitboxes;
using UML_Editor.NodeStructure;
using UML_Editor.Rendering;
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class LabelNode : BasicNode, ITextNode
    {
        public new BasicTextNodeStructure Structure { get; set; }
        public TextRenderElementStyle TextStyle { get; }

        private TextRenderElement TextElement;

        public LabelNode(BasicTextNodeStructure structure, TextRenderElementStyle text_style,
            RectangleRenderElementStyle border_style) : base(structure, border_style)
        {
            Structure = structure;
            TextStyle = text_style;
            TextElement = new TextRenderElement(Position, Text, text_style.Color, text_style.FontSize, text_style.FontStyle);
        }
        public string Text
        {
            get => Structure.Text;
            set
            {
                Structure.Text = value;
                TextElement.Text = value;
                Width = Renderer.GetTextWidth(value.Length);
                OnTextChange?.Invoke(this, new TextEventArgs(Text));
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public override Vector Position
        {
            get => base.Position;
            set
            {
                TextElement.Position = value;
                base.Position = value;
            }
        }

        public Color TextColor
        {
            get => TextStyle.Color;
            set
            {
                TextStyle.Color = value;
                TextElement.Color = value;
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }
        public FontStyle Style
        {
            get => TextStyle.FontStyle;
            set
            {
                TextStyle.FontStyle = value;
                TextElement.FontStyle = value;
                OnChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Render(Renderer renderer)
        {
            base.Render(renderer);
            TextElement.Render(renderer);
        }
        public EventHandler<TextEventArgs> OnTextChange { get; set; }
    }
}
