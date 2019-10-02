using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Enums;
using UML_Editor.EventArguments;
using UML_Editor.Hitboxes;
using UML_Editor.Geometry;
using UML_Editor.NodeStructure;
using UML_Editor.ProjectStructure;
using UML_Editor.Rendering.ElementStyles;

namespace UML_Editor.Relationships
{
    public class Relationship
    {
        public RelationshipSegment Origin { get; set; }
        public RelationshipSegment Target { get; set; }
        public string Name { get; set; }

        public List<IHitbox> TriggerAreas { get; set; }
        public EventHandler<ResizeEventArgs> OnResize { get; set; }
        public EventHandler OnFocused { get; set; }
        public EventHandler OnUnfocused { get; set; }
        public BasicContainerNode OptionsPrefab { get; set; }
        public BasicContainerNode OptionsMenu { get; set; }
        public bool isFocused { get; set; }

        private ClassDiagramNode OriginNode;
        private ClassDiagramNode TargetNode;
        public Relationship(ClassDiagramNode origin, ClassDiagramNode target)
        {
            OriginNode = origin;
            TargetNode = target;
            List<Vector> vectors = GetLineVectors();
            Origin = new RelationshipSegment(vectors[0], vectors[3], vectors[2]);
            Target = new RelationshipSegment(vectors[1], vectors[4], vectors[2]);
            OriginNode.OnPositionChanged += OnPositionChanged;
            TargetNode.OnPositionChanged += OnPositionChanged;
            OnOptionsHide += HideOptions;
            OnOptionsShow += ShowOptions;
            GeneratePrefab();
            StealHitboxes();
        }
        public void Render(Renderer renderer)
        {
            Target.Render(renderer);
            Origin.Render(renderer);
            OptionsMenu?.Render(renderer);
        }

        private void StealHitboxes()
        {
            TriggerAreas = new List<IHitbox>();
            TriggerAreas.AddRange(Origin.TriggerAreas);
            TriggerAreas.AddRange(Target.TriggerAreas);
        }
       
        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            List<Vector> vectors = GetLineVectors();
            Origin.Position = vectors[0];
            Origin.Joint = vectors[3];
            Origin.Midpoint = vectors[2];

            Target.Position = vectors[1];
            Target.Joint = vectors[4];      
            Target.Midpoint = vectors[2];
            StealHitboxes();
        }

        private List<Vector> GetLineVectors()
        {
            List<Vector> Values = new List<Vector>();
            Line CntrLine = new Line(TargetNode.GetCenter(), OriginNode.GetCenter());

            if (Šafránková.Intersect(CntrLine, OriginNode.GetRightSide()))
                Values.Add(OriginNode.GetRightAnchor());
            else if (Šafránková.Intersect(CntrLine, OriginNode.GetTopSide()))
                Values.Add(OriginNode.GetTopAnchor());
            else if (Šafránková.Intersect(CntrLine, OriginNode.GetLeftSide()))
                Values.Add(OriginNode.GetLeftAnchor());
            else
                Values.Add(OriginNode.GetBotAnchor());

            if (Šafránková.Intersect(CntrLine, TargetNode.GetRightSide()))
                Values.Add(TargetNode.GetRightAnchor());
            else if (Šafránková.Intersect(CntrLine, TargetNode.GetTopSide()))
                Values.Add(TargetNode.GetTopAnchor());
            else if (Šafránková.Intersect(CntrLine, TargetNode.GetLeftSide()))
                Values.Add(TargetNode.GetLeftAnchor());
            else
                Values.Add(TargetNode.GetBotAnchor());

            Values.Add((Values[0] + Values[1]) / 2);
            if (Values[0] == OriginNode.GetLeftAnchor() || Values[0] == OriginNode.GetRightAnchor())
                Values.Add(new Vector(Values[2].X, Values[0].Y));
            else
                Values.Add(new Vector(Values[0].X, Values[2].Y));

            if (Values[1] == TargetNode.GetLeftAnchor() || Values[1] == TargetNode.GetRightAnchor())
                Values.Add(new Vector(Values[2].X, Values[1].Y));
            else
                Values.Add(new Vector(Values[1].X, Values[2].Y));

            return Values;
        }

        public List<INode> GetChildren()
        {
            List<INode> Children = new List<INode>();
            if (OptionsMenu != null)
                Children.Add(OptionsMenu);
            return Children;
        }

        public IFocusableNode FocusedNode { get; set; }

        public void GeneratePrefab()
        {
            float total_Width = Renderer.GetTextWidth(13);
            OptionsPrefab = new BasicContainerNode(new BasicNodeStructure(Vector.Zero, total_Width, Renderer.SingleTextHeight * 3), RectangleRenderElementStyle.Default);
            OptionsPrefab.AddNode(new ButtonNode(new ButtonStructure(Vector.Zero, "Add Property", total_Width, Renderer.SingleTextHeight, () =>
                {
                    OnOptionsHide?.Invoke(this, EventArgs.Empty);
                }),
                RectangleRenderElementStyle.Default,
                TextRenderElementStyle.Default));
        }

        public void ShowOptions(object sender, EventArgs e)
        {
            if (OptionsMenu == null)
            {
                OptionsMenu = OptionsPrefab;
                OnFocused?.Invoke(this, new NodeEventArgs(OptionsMenu));
                FocusedNode?.OnUnfocused?.Invoke(this, new NodeEventArgs(FocusedNode));
            }
            else
                OnOptionsHide?.Invoke(this, e);
        }
        public void HideOptions(object sender, EventArgs e)
        {
            OptionsMenu = null;
        }

        public EventHandler OnOptionsShow { get; set; }
        public EventHandler OnOptionsHide { get; set; }

        public EventHandler OnMouseClick { get; set; }
    }
}
