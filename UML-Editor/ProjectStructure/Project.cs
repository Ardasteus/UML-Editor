using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.ProjectStructure
{
    public class Project
    {
        public string Name { get; set; }
        public List<ClassStructure> Classes { get; set; }
        public List<RelationshipStructure> Relationships { get; set; }
        public void AddClass(ClassStructure klass) => Classes.Add(klass);
        public void AddRelationship(ClassStructure origin, ClassStructure target) => Relationships.Add(new RelationshipStructure(origin, target));

        public Project(string name)
        {
            name = Name;
            Classes = new List<ClassStructure>();
            Relationships = new List<RelationshipStructure>();
        }
    }
}
