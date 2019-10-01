using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using System.Windows.Forms;
using UML_Editor.EventArguments;
using UML_Editor.Hitboxes;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Nodes
{
    public class TextBoxNode : LabelNode, IKeyboardFocusableNode
    {
        public TextBoxNode(BasicTextNodeStructure structure, TextRenderElementStyle text_style,
            RectangleRenderElementStyle border_style) : base(structure, text_style, border_style)
        {
            TriggerAreas.Add(new RectangleHitbox(Position, Width, Height));
            OnKeyPress += HandleKey;
            OnFocused += (sender, args) => FillColor = Color.Blue;
            OnUnfocused += (sender, args) => FillColor = Color.White;
        }

        private void HandleKey(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (key == (char)8 && Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
                Width = Renderer.GetTextWidth(Text.Length);
            }
            else if (key == (char)13)
                OnUnfocused?.Invoke(this, new NodeEventArgs(this));
            else if (Char.IsWhiteSpace(key))
                Text = Text.Insert(Text.Length, " ");
            else
                Text = Text.Insert(Text.Length, key.ToString());
        }

        public EventHandler<KeyPressEventArgs> OnKeyPress { get; set; }
        public EventHandler<NodeEventArgs> OnFocused { get; set; }
        public EventHandler<NodeEventArgs> OnUnfocused { get; set; }
    }
}
