using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.ProjectStructure
{
    public class RelationshipStructure
    {
        public RelationshipStructure(ClassStructure origin, ClassStructure target)
        {
            Origin = origin;
            Target = target;
        }

        public ClassStructure Origin { get; set; }
        public ClassStructure Target { get; set; }
    }
}
