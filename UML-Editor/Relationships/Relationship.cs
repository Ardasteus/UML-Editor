using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Rendering;

namespace UML_Editor.Relationships
{
    public class Relationship
    {
        public Vector Position { get; set; }
        public RelationshipSegment From { get; set; }
        public RelationshipSegment To { get; set; }
        public Relationship(ClassDiagramNode Origin, ClassDiagramNode Target)
        {
            Position = (Origin.Position + Target.Position) / 2;
            From = new RelationshipSegment(Position, Origin);
            To = new RelationshipSegment(Position, Target);
        }
        public void ChangeOrigin(ClassDiagramNode classDiagram)
        {
            From.TargetNode = classDiagram;
        }
        public void ChangeDestination(ClassDiagramNode classDiagram)
        {
            To.TargetNode = classDiagram;
        }
        public void Render(Renderer renderer)
        {
            To.Render(renderer);
            From.Render(renderer);
        }
    }
}
