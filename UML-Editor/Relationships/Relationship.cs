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

namespace UML_Editor.Relationships
{
    public class Relationship
    {
        public Vector Position { get; set; }
        public RelationshipSegment Origin { get; set; }
        public RelationshipSegment Target { get; set; }
        private ClassDiagramNode OriginNode;
        private ClassDiagramNode TargetNode;
        public LineRenderElement Centerline { get; set; }
        public Relationship(ClassDiagramNode origin, ClassDiagramNode target)
        {
            OriginNode = origin;
            TargetNode = target;
            List<Vector> vectors = GetLineVectors();
            Centerline = new LineRenderElement(vectors[0], vectors[1], 1, Color.Black);
            OriginNode.OnPositionChanged += OnPositionChanged;
            TargetNode.OnPositionChanged += OnPositionChanged;
        }
        public void Render(Renderer renderer)
        {
            //Target.Render(renderer);
            //Origin.Render(renderer);
            Centerline.Render(renderer);
        }
       
        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            List<Vector> vectors = GetLineVectors();
            Centerline = new LineRenderElement(vectors[0], vectors[1], 1, Color.Black);
        }

        private List<Vector> GetLineVectors()
        {
            List<Vector> Values = new List<Vector>();
            Vector OriginClosest = Vector.Zero;
            Vector TargetClosest = Vector.Zero;
            foreach (Vector org in OriginNode.GetSideCenters())
            {
                foreach (Vector tar in TargetNode.GetSideCenters())
                {
                    if (OriginClosest == Vector.Zero)
                        OriginClosest = org;
                    if (TargetClosest == Vector.Zero)
                        TargetClosest = tar;

                    if(Vector.GetDistance(tar - org) < Vector.GetDistance(OriginClosest - TargetClosest))
                    {
                        OriginClosest = org;
                        TargetClosest = tar;
                    }
                }
            }
            Values.Add(OriginClosest);
            Values.Add(TargetClosest);
            Values.Add((OriginClosest + TargetClosest) / 2);
            return Values;
        }

    }
}
