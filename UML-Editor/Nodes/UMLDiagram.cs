using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Enums;
using UML_Editor.Rendering.RenderingElements;
using UML_Editor.Geometry;

namespace UML_Editor.Nodes
{
    public abstract class UMLDiagram : IRenderableNode, IContainerNode
    {
        public string Name { get; set; }
        public abstract Vector Position { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public List<IHitbox> TriggerAreas { get; set; } = new List<IHitbox>();
        public TextBoxNode NameTextBox { get; set; }
        public RectangleRenderElement BorderElement { get; set; }

        public abstract List<INode> GetChildren();

        public abstract void Render(Renderer renderer);
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        public EventHandler<DiagramRemovalEventArgs> OnRemoval;
    }
}
