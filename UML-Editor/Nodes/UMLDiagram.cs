using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Others;
using UML_Editor.Rendering;
using UML_Editor.Enums;

namespace UML_Editor.Nodes
{
    public abstract class UMLDiagram : IRenderableNode, IContainerNode
    {
        public string Name { get; set; }
        public abstract Vector Position { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public bool Resize { get; set; }
        public List<IHitbox> TriggerAreas { get; set; }
        public List<PropertyNode> Properties { get; set; } = new List<PropertyNode>();
        public List<MethodNode> Methods { get; set; } = new List<MethodNode>();
        public Modifiers Modifiers { get; set; }

        public void ForceResize(int width)
        {
        }
        public abstract void AddProperty();
        public abstract void AddMethod();
        public abstract List<INode> GetChildren();

        public abstract void Render(Renderer renderer);
    }
}
