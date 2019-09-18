using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;

namespace UML_Editor.Nodes
{
    public class LabelNode : IRenderableNode
    {
        public string Name { get; set; }
        public Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
            }
        }
        public int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
                OnResize?.Invoke();
            }
        }
        public int Height
        {
            get => BorderElement.Height;
            set
            {
                BorderElement.Height = value;
                ((RectangleHitbox)TriggerAreas[0]).Height = value;
            }
        }
        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        private RectangleRenderElement BorderElement;
        private TextRenderElement TextElement;


        public void ForceResize(int width)
        {
        }

        public void Render(Renderer renderer)
        {
        }
        public Action OnResize { get; set; }
        public Action OnFocused { get; set; }
        public Action OnUnfocused { get; set; }
    }
}
