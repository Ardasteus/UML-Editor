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
        List<LineRenderElement> Lines = new List<LineRenderElement>();
        public Relationship(ClassDiagramNode origin, ClassDiagramNode target)
        {
            Vector originPos = origin.Position + new Vector(origin.Width / 2, origin.Height / 2);
            Vector targetPos = target.Position + new Vector(target.Width / 2, target.Height / 2);
            Position = (originPos + targetPos) / 2;
            Vector JointOrigin = ((RectangleHitbox)origin.TriggerAreas[0]).DeterminePosition(Position);
            Vector JointTarget = ((RectangleHitbox)target.TriggerAreas[0]).DeterminePosition(Position);
            Position.X = JointOrigin.X;
            Position.Y = JointTarget.Y;
            if (origin.IsOnEdge(Position) || target.IsOnEdge(Position))
            {
                Position.X = JointTarget.X;
                Position.Y = JointOrigin.Y;
            }

            Origin = new RelationshipSegment(Position, JointOrigin, origin);
            Target = new RelationshipSegment(Position, JointTarget, target);
            Origin.OnAnchorRequest += SetAnchor;
            Target.OnAnchorRequest += SetAnchor;
            Target.SetRelationshipType();
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
        
        private void SetAnchor(object sender, EventArgs e)
        {
            ClassDiagramNode origin = Origin.TargetNode;
            ClassDiagramNode target = Target.TargetNode;
            Vector originPos = origin.Position + new Vector(origin.Width / 2, origin.Height / 2);
            Vector targetPos = target.Position + new Vector(target.Width / 2, target.Height / 2);
            Position = (originPos + targetPos) / 2;
            Vector JointOrigin = ((RectangleHitbox)origin.TriggerAreas[0]).DeterminePosition(Position);
            Vector JointTarget = ((RectangleHitbox)target.TriggerAreas[0]).DeterminePosition(Position);
            Position.X = JointOrigin.X;
            Position.Y = JointTarget.Y;
            if (origin.IsOnEdge(Position) || target.IsOnEdge(Position))
            {
                Position.X = JointTarget.X;
                Position.Y = JointOrigin.Y;
            }

            Origin.Position = Position;
            Origin.AnchorPosition = JointOrigin;
            Target.Position = Position;
            Target.AnchorPosition = JointTarget;
        }

        private void SetVectors(ClassDiagramNode origin, ClassDiagramNode target)
        {
            Vector originPos = origin.Position + new Vector(origin.Width / 2, origin.Height / 2);
            Vector targetPos = target.Position + new Vector(target.Width / 2, target.Height / 2);
            Position = (originPos + targetPos) / 2;
            Vector JointOrigin = ((RectangleHitbox)origin.TriggerAreas[0]).DeterminePosition(Position);
            Vector JointTarget = ((RectangleHitbox)target.TriggerAreas[0]).DeterminePosition(Position);
            Position.X = JointOrigin.X;
            Position.Y = JointTarget.Y;
            if (origin.IsOnEdge(Position) || target.IsOnEdge(Position))
            {
                Position.X = JointTarget.X;
                Position.Y = JointOrigin.Y;
            }
        }
    }
}
