using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Rendering;

namespace UML_Editor.Others
{
    public class RectangleHitbox : IHitbox
    {
        public RectangleHitbox(Vector position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector Position { get; set; }
        public bool IsTriggerable { get; set; } = true;

        public bool HasTriggered(Vector position)
        {
            int left = Position.X;
            int right = Position.X + Width;
            int top = Position.Y;
            int bot = Position.Y + Height;
            return position.X <= right && position.X >= left && position.Y <= bot && position.Y >= top;
        }
    }
}
