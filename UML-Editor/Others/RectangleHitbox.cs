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
        
        public Vector DeterminePosition(Vector middle_point)
        {
            int left = Position.X;
            int right = Position.X + Width;
            int top = Position.Y;
            int bot = Position.Y + Height;
            //Left
            if (middle_point.Y <= bot && middle_point.Y >= top)
            {
                if (middle_point.X > left && middle_point.X > right)
                    return new Vector(right, middle_point.Y);
                else
                    return new Vector(left, middle_point.Y);
            }
            else if (middle_point.X <= right && middle_point.X >= left)
            {
                if (middle_point.Y > top && middle_point.Y > bot)
                    return new Vector(middle_point.X, bot);
                else
                    return new Vector(middle_point.X, top);
            }
            else
            {
                if (middle_point.X < left && middle_point.Y < bot)
                {
                    return new Vector(left, (bot + top) / 2);
                }
                else if (middle_point.X > right && middle_point.Y < bot)
                {
                    return new Vector(right, (bot + top) / 2);
                }
                else if (middle_point.X < left && middle_point.Y < bot)
                {
                    return new Vector((right + left) / 2, top);
                }
                else
                {
                    return new Vector((right + left) / 2, bot);
                }
            }
        }
    }
}
