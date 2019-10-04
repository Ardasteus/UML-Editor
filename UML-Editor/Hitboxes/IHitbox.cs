using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;
using UML_Editor.Rendering;

namespace UML_Editor.Hitboxes
{
    public interface IHitbox
    {
        bool HasTriggered(Vector position);
        bool IsTriggerable { get; set; }
    }
}
