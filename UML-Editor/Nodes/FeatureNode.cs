using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;
using UML_Editor.Enums;

namespace UML_Editor.Nodes
{
    public abstract class FeatureNode : IRenderableNode, IContainerNode
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Resize { get; set; }
        public AccessModifiers AccessModifie { get; set; }
        public Modifiers Modifier { get; set; }

        public abstract void ForceResize(int width);

        public abstract List<INode> GetChildren();

        public abstract void Render(Renderer renderer);
    }
}
