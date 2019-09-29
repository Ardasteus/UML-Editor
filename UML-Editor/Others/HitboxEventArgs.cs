using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Editor.Others
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
