    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Nodes;
using UML_Editor.Rendering;

namespace UML_Editor.Relationships
{
    public class RelationshipManager
    {
        public List<Relationship> Relationships = new List<Relationship>();
        public bool IsCreating = false;

        private ClassDiagramNode node = null;
        public ClassDiagramNode SelectedNode
        {
            get => node;
            set
            {
                if (node == null)
                    node = value;
                else if(node != value)
                {
                    CreateRelationship(node, value);
                    node = null;
                }
            }
        }
        public void CreateRelationship(ClassDiagramNode from, ClassDiagramNode to)
        {
            Relationships.Add(new Relationship(from, to));
        }
        public void Render(Renderer renderer)
        {
            Relationships.ForEach(x => x.Render(renderer));
        }


    }
}
