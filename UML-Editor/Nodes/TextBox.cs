using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;

namespace UML_Editor.Nodes
{
    public class TextBox : IRenderableNode, IKeyboardHandlerNode, IMouseHandlerNode
    {
        public TextBox(string name, string text, Vector position, int width, int height Color text_color, Color border_color, Color fill_color, int border_width = 1)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            TriggerAreaWidth = width;
            TriggerAreaHeight = height;
            BorderColor = border_color;
            FillColor = fill_color;
            Generate();
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public Vector Position { get; set; }
        public List<INode> ChildNodes { get; set; }
        public int TriggerAreaWidth { get; set; }
        public int TriggerAreaHeight { get; set; }
        public bool isFocused { get; set; } = false;
        private Color borderColor;
        public Color BorderColor { get; set; }
        private Color fillColor;
        public Color FillColor { get; set; }
        private int borderWidth;
        public int BorderWidth { get; set; }
        private Color textColor;
        public Color TextColor { get; set; }

        private RectangleRenderElement BorderElement;
        private TextRenderElement TextElement;

        public void Generate()
        {
            BorderElement = new RectangleRenderElement(Position, TriggerAreaWidth, TriggerAreaHeight, BorderColor, FillColor);
            TextElement = new TextRenderElement(Position, Name, TextColor);
        }

        public void Render(Renderer renderer)
        {

        }

        public void HandleKey()
        {
        }

        public void HandleMouse()
        {
        }
    }
}
