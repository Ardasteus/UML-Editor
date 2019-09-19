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
using UML_Editor.Nodes.Interfaces;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Nodes
{
    public class ClassDiagramNode : UMLDiagram, IOptionsNode
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
                OnResize?.Invoke(this, new ResizeEventArgs(Width));
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
        public ContextMenuNode OptionsPrefab { get; set; }
        public ContextMenuNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }

        public ClassDiagramNode(Vector position, string Name, Modifiers modifier, AccessModifiers accessModifiers)
        {
            BorderElement = new RectangleRenderElement(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight));
            NameTextBox = new TextBoxNode("diagram_name", Name, Position, Width, Renderer.SingleTextHeight, Color.Black, Color.Black, Color.White);
            NameTextBox.OnResize = NameResize;
            GeneratePrefab();
        }

        public void AddProperty(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            PropertyNode new_prop = new PropertyNode("prop", Position + new Vector(0, (Methods.Count + Properties.Count + 1) * Renderer.SingleTextHeight), type, name, accessModifier, modifier);
            new_prop.OnResize = Resize;
            Properties.Add(new_prop);
            Height += Renderer.SingleTextHeight;
            Resize(this, new ResizeEventArgs(new_prop.Width));

        }
        public void AddMethod(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            MethodNode new_method = new MethodNode("prop", Position + new Vector(0, (Methods.Count + Properties.Count + 1) * Renderer.SingleTextHeight), type, name, accessModifier, modifier);
            new_method.OnResize = Resize;
            Methods.Add(new_method);
            Height += Renderer.SingleTextHeight;
            Resize(this, new ResizeEventArgs(new_method.Width));
        }

        private void Resize(object sender, ResizeEventArgs args)
        {
            if(args.Width > Width)
            {
                Width = args.Width;
            }
            else
            {
                INode temp_node = GetChildren().OrderByDescending(x => x.Width).FirstOrDefault();
                if(temp_node.Width < Width)
                {
                    Width = temp_node.Width;
                }
            }
            NameTextBox.Position = new Vector((Position.X + Width / 2) - (NameTextBox.Width / 2), Position.Y);
        }

        private void NameResize(object sender, ResizeEventArgs args)
        {
            if(args.Width / 2 > Width / 2)
            {
                Width = args.Width;
            }
            else
            {
                INode temp_node = GetChildren().OrderByDescending(x => x.Width).FirstOrDefault();
                if (temp_node.Width < Width)
                {
                    Width = temp_node.Width;
                }
            }
            NameTextBox.Position = new Vector((Position.X + Width / 2) - (NameTextBox.Width / 2), Position.Y);
        }

        public override List<INode> GetChildren()
        {
            List<INode> ret = new List<INode>();
            ret.Add(NameTextBox);
            ret.AddRange(Properties);
            ret.AddRange(Methods);
            if (OptionsMenu != null)
                ret.Add(OptionsMenu);
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            NameTextBox.Render(renderer);
            Properties.ForEach(x => x.Render(renderer));
            Methods.ForEach(x => x.Render(renderer));
            OptionsMenu?.Render(renderer);
        }

        public void GeneratePrefab()
        {
            OptionsPrefab = new ContextMenuNode("cnt", Vector.Zero, 0, 0, RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Add Property", Vector.Zero, Renderer.GetTextWidth(12), Renderer.SingleTextHeight, () =>
            {
                AddProperty("Property", "Type", AccessModifiers.Public, Modifiers.None);
                OptionsMenu = null;
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Add Method", Vector.Zero, Renderer.GetTextWidth(10), Renderer.SingleTextHeight, () =>
            {
                OptionsMenu = null;
                AddMethod("Method", "Type", AccessModifiers.Public, Modifiers.None);
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Make into an interface", Vector.Zero, Renderer.GetTextWidth(22), Renderer.SingleTextHeight, () =>
            {
                OptionsMenu = null;
                TriggerAreas.RemoveAt(1);
                GeneratePrefab();
            },
            RectangleRenderElementStyle.Default));
        }

        public void ShowMenu()
        {
            if (OptionsMenu == null)
            {
                TriggerAreas.Add(new RectangleHitbox(OptionsPrefab.Position, OptionsPrefab.Width, OptionsPrefab.Height));
                OptionsMenu = OptionsPrefab;
            }
            else
            {
                TriggerAreas.RemoveAt(1);
                OptionsMenu = null;
            }
        }

        public void HandleMouse()
        {
        }
    }
}
