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
        protected FeatureNode(string name, Vector position, AccessModifiers accessModifier, Modifiers modifier)
        {
            Name = name;
            Position = position;
            AccessModifier = accessModifier;
            Modifier = modifier;
        }

        public string Name { get; set; }
        public Vector Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Resize { get; set; }
        public AccessModifiers AccessModifier { get; set; }
        public Modifiers Modifier { get; set; }

        public abstract void ForceResize(int width);

        public string GetModifierChar()
        {
            switch (AccessModifier)
            {
                case AccessModifiers.Private:
                    return "-";
                case AccessModifiers.Public:
                    return "+";
                case AccessModifiers.Protected:
                    return "#";
                default:
                    return "E";
            }
        }

        public abstract List<INode> GetChildren();

        public abstract void Render(Renderer renderer);
    }
}
