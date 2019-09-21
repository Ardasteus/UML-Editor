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
        private FeatureNode FocusedFeature;
        private LineRenderElement NameLine;
        private LineRenderElement SeparatorLine;
        public override Vector Position
        {
            get => BorderElement.Position;
            set
            {
                BorderElement.Position = value;
                ((RectangleHitbox)TriggerAreas[0]).Position = value;
                Resize(this, new ResizeEventArgs(Width));
                OnPositionChanged?.Invoke(this, new PositionEventArgs(Position));
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
        public EventHandler<PositionEventArgs> OnPositionChanged { get; set; }

        public ClassDiagramNode(Vector position, string Name, Modifiers modifier, AccessModifiers accessModifiers)
        {
            BorderElement = new RectangleRenderElement(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Renderer.GetTextWidth(Name.Length), Renderer.SingleTextHeight));
            NameTextBox = new TextBoxNode("diagram_name", Name, Position, Width, Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            NameTextBox.OnResize = NameResize;
            NameLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            SeparatorLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            GeneratePrefab();
        }

        public void AddProperty(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            PropertyNode new_prop = new PropertyNode("prop", Position + new Vector(0, (Properties.Count + 1) * Renderer.SingleTextHeight), type, name, accessModifier, modifier);
            new_prop.OnResize = Resize;
            new_prop.OnFocused = OnFeatureFocused;
            new_prop.OnUnfocused = OnFeatureUnfocused;
            Properties.Add(new_prop);
            Height += Renderer.SingleTextHeight;
            Resize(this, new ResizeEventArgs(new_prop.Width));
        }
        public void AddMethod(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            MethodNode new_method = new MethodNode("prop", Position + new Vector(0, (Methods.Count + Properties.Count + 1) * Renderer.SingleTextHeight), type, name, accessModifier, modifier);
            new_method.OnResize = Resize;
            new_method.OnFocused = OnFeatureFocused;
            new_method.OnUnfocused = OnFeatureUnfocused;
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
            for (int i = 0; i < Properties.Count; i++)
            {
                Properties[i].Position = Position + new Vector(0, (i + 1) * Renderer.SingleTextHeight);
            }
            for (int i = 0; i < Methods.Count; i++)
            {
                Methods[i].Position = Position + new Vector(0, (i + Properties.Count + 1) * Renderer.SingleTextHeight);
            }
            NameLine.StartPoint = new Vector(Position.X, Position.Y + Renderer.SingleTextHeight);
            NameLine.EndPoint = new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight);
            if(Properties.Count > 0)
            {
                PropertyNode prop = Properties.Last();  
                SeparatorLine.StartPoint = prop.Position + new Vector(0, Renderer.SingleTextHeight);
                SeparatorLine.EndPoint = prop.Position + new Vector(Width, Renderer.SingleTextHeight);
            }
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
            if (OptionsMenu != null)
                ret.Add(OptionsMenu);
            if (FocusedFeature != null)
                ret.Add(FocusedFeature);
            ret.Add(NameTextBox);
            ret.AddRange(Properties.Where(x => x != FocusedFeature));
            ret.AddRange(Methods.Where(x => x != FocusedFeature));
            return ret;
        }

        public override void Render(Renderer renderer)
        {
            BorderElement.Render(renderer);
            NameTextBox.Render(renderer);
            Properties.ForEach(x => x.Render(renderer));
            Methods.ForEach(x => x.Render(renderer));
            NameLine.Render(renderer);
            SeparatorLine.Render(renderer);
            FocusedFeature?.AccessModifiersContextMenu?.Render(renderer);
            BorderElement.BorderOnly(renderer);
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
                FocusedFeature?.OnUnfocused?.Invoke(this, new EventArgs());
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

        private void OnFeatureFocused(object sender, EventArgs e)
        {
            if(FocusedFeature == null)
            {
                FocusedFeature = (FeatureNode)sender;
                if(OptionsMenu != null)
                {
                    TriggerAreas.RemoveAt(1);
                    OptionsMenu = null;
                }
            }
            else
            {
                FocusedFeature.OnUnfocused(this, new EventArgs());
                FocusedFeature = (FeatureNode)sender;
            }
        }
        private void OnFeatureUnfocused(object sender, EventArgs e)
        {
            if (FocusedFeature?.AccessModifiersContextMenu != null)
                FocusedFeature.ShowMenu();
            FocusedFeature = null;
        }
    }
}
