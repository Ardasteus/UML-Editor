using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;
using UML_Editor.Others;
using UML_Editor.Geometry;

namespace UML_Editor.Relationships
{
    public class Relationship
    {
        public Vector Position { get; set; }
        public RelationshipSegment Origin { get; set; }
        public RelationshipSegment Target { get; set; }
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
        }
        public void Render(Renderer renderer)
        {
            Target.Render(renderer);
            Origin.Render(renderer);
        }
       
        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            List<Vector> vectors = GetLineVectors();
            Origin.Position = vectors[0];
            Origin.JointPosition = vectors[3];
            Origin.AnchorPosition = vectors[2];

            Target.Position = vectors[1];
            Target.JointPosition = vectors[4];
            Target.AnchorPosition = vectors[2];
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
            else if (Šafránková.Intersect(CntrLine, OriginNode.GetBotSide()))
                Values.Add(OriginNode.GetBotAnchor());

            if (Šafránková.Intersect(CntrLine, TargetNode.GetRightSide()))
                Values.Add(TargetNode.GetRightAnchor());
            else if (Šafránková.Intersect(CntrLine, TargetNode.GetTopSide()))
                Values.Add(TargetNode.GetTopAnchor());
            else if (Šafránková.Intersect(CntrLine, TargetNode.GetLeftSide()))
                Values.Add(TargetNode.GetLeftAnchor());
            else if (Šafránková.Intersect(CntrLine, TargetNode.GetBotSide()))
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
    }
}
