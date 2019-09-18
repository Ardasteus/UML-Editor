using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Enums;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Others;

namespace UML_Editor.Nodes
{
    public class ClassDiagramNode : UMLDiagram
    {
        public override Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
            }
        }
        public override int Width
        {
            get => BorderElement.Width;
            set
            {
                BorderElement.Width = value;
                ((RectangleHitbox)TriggerAreas[0]).Width = value;
                OnResize?.Invoke();
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
        private List<PropertyNode> Properties = new List<PropertyNode>();
        private List<MethodNode> Methods = new List<MethodNode>();
        public Modifiers Modifier { get; set; }
        public AccessModifiers AccessModifier { get; set; }
        public ClassDiagramNode(Vector position, string Name, Modifiers modifier, AccessModifiers accessModifiers)
        {
            BorderElement = new RectangleRenderElement(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight * 3, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight * 3));
            NameTextBox = new TextBoxNode("diagram_name", Name, Position, Width, Renderer.SingleTextHeight, Color.Black, Color.Black, Color.White);
        }

        public void AddProperty(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            Properties.Add(new PropertyNode("prop", Position + new Vector(0, Renderer.SingleTextHeight), type, name, accessModifier, modifier));
        }
        public void AddMethod(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            Methods.Add(new MethodNode("prop", Position + new Vector(0, Renderer.SingleTextHeight), type, name, accessModifier, modifier));
        }

        public override List<INode> GetChildren()
        {
            List<INode> ret = new List<INode>();
            ret.AddRange(Properties);
            ret.AddRange(Methods);
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            NameTextBox.Render(renderer);
            Properties.ForEach(x => x.Render(renderer));
            Methods.ForEach(x => x.Render(renderer));
        }
    }
}
