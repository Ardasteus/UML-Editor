using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Rendering;
using UML_Editor.Rendering.RenderingElements;
using System.Drawing;

namespace UML_Editor.Relationships
{
    public class Relationship
    {
        public Vector Position { get; set; }
        public RelationshipSegment Origin { get; set; }
        public RelationshipSegment Target { get; set; }
        List<LineRenderElement> Lines = new List<LineRenderElement>();
        public Relationship(ClassDiagramNode origin, ClassDiagramNode target)
        {
            Vector originPos = origin.Position + new Vector(origin.Width / 2, origin.Height / 2);
            Vector targetPos = target.Position + new Vector(target.Width / 2, target.Height / 2);
            Position = (originPos + targetPos) / 2;
            Vector JoinTargetrigin = null;
            Vector Jointtarget = null;
            int DistanceoriginX = Math.Abs(originPos.X - Position.X);
            int DistanceoriginY = Math.Abs(originPos.Y - Position.Y);
            if(DistanceoriginX > DistanceoriginY)
            {
                JoinTargetrigin = new Vector(Position.X, originPos.Y);
            }
            else
            {
                JoinTargetrigin = new Vector(originPos.X, Position.Y);
            }

            Lines.Add(new LineRenderElement(Position, JoinTargetrigin, 1, Color.Black));
            Lines.Add(new LineRenderElement(JoinTargetrigin, originPos, 1, Color.Black));
            Origin = new RelationshipSegment(Position, origin);
            Target = new RelationshipSegment(Position, target);
        }
        public void Changeorigin(ClassDiagramNode classDiagram)
        {
            Origin.TargetNode = classDiagram;
        }
        public void ChangeDestination(ClassDiagramNode classDiagram)
        {
            Target.TargetNode = classDiagram;
        }
        public void Render(Renderer renderer)
        {
            Target.Render(renderer);
            Origin.Render(renderer);
            Lines.ForEach(x => x.Render(renderer));
        }
        private void DeterminePosition()
        {
        }
    }
}
