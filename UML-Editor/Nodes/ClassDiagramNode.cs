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
using UML_Editor.Rendering.ElementStyles;
using UML_Editor.Geometry;
using UML_Editor.ProjectStructure;

namespace UML_Editor.Nodes
{
    public class ClassDiagramNode : UMLDiagram, IOptionsNode
    {
        public ClassStructure Structure;
        private FeatureNode FocusedFeature;
        private LineRenderElement NameLine;
        private LineRenderElement SeparatorLine;
        public override string Name
        {
            get => NameTextBox.Text;
            set
            {
                NameTextBox.Text = value;
                Structure.Name = value;
            }
        }
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
                OnResize?.Invoke(this, new ResizeEventArgs(Height));
            }
        }
        public List<PropertyNode> Properties = new List<PropertyNode>();
        public List<MethodNode> Methods = new List<MethodNode>();
        public Modifiers Modifier { get; set; }
        public AccessModifiers AccessModifier { get; set; }
        public ContextMenuNode OptionsPrefab { get; set; }
        public ContextMenuNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }
        public EventHandler<PositionEventArgs> OnPositionChanged { get; set; }

        public ClassDiagramNode(Vector position, ClassStructure structure)
        {
            Structure = structure;
            BorderElement = new RectangleRenderElement(position, Renderer.GetTextWidth(structure.Name.Length), Renderer.SingleTextHeight, Color.White, Color.Black);
            TriggerAreas.Add(new RectangleHitbox(position, Renderer.GetTextWidth(structure.Name.Length), Renderer.SingleTextHeight));
            NameTextBox = new TextBoxNode("diagram_name", structure.Name, Position, Width, Renderer.SingleTextHeight, Color.Black, Color.White, Color.White);
            NameTextBox.OnResize = NameResize;
            NameLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            SeparatorLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
            GeneratePrefab();
        }

        public void AddProperty(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            PropertyNode new_prop = new PropertyNode(Position + new Vector(0, (Properties.Count + 1) * Renderer.SingleTextHeight), new PropertyStructure(name, type, accessModifier, modifier));
            new_prop.OnResize = Resize;
            new_prop.OnFocused = OnFeatureFocused;
            new_prop.OnUnfocused = OnFeatureUnfocused;
            new_prop.OnHitboxCreate += OnHitboxCreation;
            new_prop.OnHitboxRemoval += OnHitboxRemoval;
            new_prop.OnRemoval += OnFeatureRemoval;
            Properties.Add(new_prop);
            Height += Renderer.SingleTextHeight;
            Resize(this, new ResizeEventArgs(new_prop.Width));
        }
        public void AddMethod(string name, string type, AccessModifiers accessModifier, Modifiers modifier)
        {
            MethodNode new_method = new MethodNode(Position + new Vector(0, (Methods.Count + Properties.Count + 1) * Renderer.SingleTextHeight), new MethodStructure(name, type, accessModifier, modifier));
            new_method.OnResize = Resize;
            new_method.OnFocused = OnFeatureFocused;
            new_method.OnUnfocused = OnFeatureUnfocused;
            new_method.OnHitboxCreate += OnHitboxCreation;
            new_method.OnHitboxRemoval += OnHitboxRemoval;
            new_method.OnRemoval += OnFeatureRemoval;
            Methods.Add(new_method);
            Height += Renderer.SingleTextHeight;
            Resize(this, new ResizeEventArgs(new_method.Width));
        }

        public void RemoveFeature(FeatureNode node)
        {
            if (node is MethodNode m)
            {
                Methods.Remove(m);
            }
            else if(node is PropertyNode p)
            {
                Properties.Remove(p);
            }
            Height -= Renderer.SingleTextHeight;
            node = null;
            Resize();
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
            Resize();
        }

        private void Resize()
        {
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
            if (Properties.Count > 0)
            {
                PropertyNode prop = Properties.Last();
                SeparatorLine.StartPoint = prop.Position + new Vector(0, Renderer.SingleTextHeight);
                SeparatorLine.EndPoint = prop.Position + new Vector(Width, Renderer.SingleTextHeight);
            }
            else
            {
                SeparatorLine = new LineRenderElement(new Vector(Position.X, Position.Y + Renderer.SingleTextHeight), new Vector(Position.X + Width, Position.Y + Renderer.SingleTextHeight), 1, Color.Black);
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
            BorderElement.BorderOnly(renderer);
            FocusedFeature?.AccessModifiersContextMenu?.Render(renderer);
            FocusedFeature?.OptionsMenu?.Render(renderer);
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
            OptionsPrefab.AddNode(new ButtonNode("btn1", "Remove", Vector.Zero, Renderer.GetTextWidth(6), Renderer.SingleTextHeight, () =>
            {
                OptionsMenu = null;
                TriggerAreas.RemoveAt(1);
                OnRemoval?.Invoke(this, new DiagramRemovalEventArgs(this));
            },
            RectangleRenderElementStyle.Default));
        }

        public void ShowOptionsMenu()
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
            if(FocusedFeature == sender)
            {
                if (OptionsMenu != null)
                {
                    TriggerAreas.RemoveAt(1);
                    OptionsMenu = null;
                }
            }
            else if(FocusedFeature == null)
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
            {
                FocusedFeature.TriggerAreas.RemoveAt(1);
                FocusedFeature.AccessModifiersContextMenu = null;
                FocusedFeature.IsMenuShown = false;
            }
            else if(FocusedFeature?.OptionsMenu != null)
            {
                FocusedFeature.TriggerAreas.RemoveAt(1);
                FocusedFeature.OptionsMenu = null;
            }
            FocusedFeature = null;
        }

        public bool IsOnEdge(Vector v)
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            return v.X == left || v.X == right || v.Y == top || v.Y == bot;
        }

        public List<Vector> GetSideCenters()
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            List<Vector> centers = new List<Vector>();
            centers.Add(new Vector((left + right) / 2, top));
            centers.Add(new Vector(right, (top + bot) / 2));
            centers.Add(new Vector((left + right) / 2, bot));
            centers.Add(new Vector(left, (top + bot) / 2));
            return centers;
        }

        public Vector GetTopAnchor() => new Vector((Position.X + (Width / 2)), Position.Y);
        public Vector GetBotAnchor() => new Vector((Position.X + (Width / 2)), Position.Y + Height);
        public Vector GetLeftAnchor() => new Vector(Position.X, Position.Y + (Height / 2));
        public Vector GetRightAnchor() => new Vector(Position.X + Width, Position.Y + (Height / 2));
        public Vector GetCenter() => new Vector(Position.X + (Width / 2), Position.Y + (Height / 2));
        public Vector GetTopLeftCorner() => new Vector(Position.X, Position.Y);
        public Vector GetTopRightCorner() => new Vector(Position.X + Width, Position.Y);
        public Vector GetBotLeftCorner() => new Vector(Position.X, Position.Y + Height);
        public Vector GetBotRightCorner() => new Vector(Position.X + Width, Position.Y + Height);
        public Line GetTopSide() => new Line(GetTopLeftCorner(), GetTopRightCorner());
        public Line GetBotSide() => new Line(GetBotLeftCorner(), GetBotRightCorner());
        public Line GetLeftSide() => new Line(GetTopLeftCorner(), GetBotLeftCorner());
        public Line GetRightSide() => new Line(GetTopRightCorner(), GetBotRightCorner());

        private void OnHitboxCreation(object sender, HitboxEventArgs e)
        {
            TriggerAreas.Add(e.Hitbox);
        }
        private void OnHitboxRemoval(object sender, HitboxEventArgs e)
        {
            TriggerAreas.Remove(e.Hitbox);
        }

        private void OnFeatureRemoval(object sender, FeatureRemovalEventArgs e)
        {
            RemoveFeature(e.FeatureNode);
        }

        public void Unfocus()
        {
            if (OptionsMenu != null)
                OptionsMenu = null;
            FocusedFeature?.OnUnfocused(this, new EventArgs());
        }
    }
}
