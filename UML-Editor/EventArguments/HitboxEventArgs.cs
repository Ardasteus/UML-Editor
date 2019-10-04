using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Hitboxes;

namespace UML_Editor
{
    public class HitboxEventArgs : EventArgs
    {
        public IHitbox Hitbox;

        public HitboxEventArgs(IHitbox hitbox)
        {
            Hitbox = hitbox;
        }
    }
}
