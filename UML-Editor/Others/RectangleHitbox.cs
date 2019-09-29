using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Editor.Geometry;
using UML_Editor.Rendering;

namespace UML_Editor.Others
{
    public class RectangleHitbox : IHitbox
    {
        public RectangleHitbox(Vector position, int width, int height)
        {
            Position = new Vector(position.X, position.Y);
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector Position { get; set; }
        public bool IsTriggerable { get; set; } = true;

        public bool HasTriggered(Vector position)
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            return position.X <= right && position.X >= left && position.Y <= bot && position.Y >= top;
        }

        public Vector GetCenter()
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            return new Vector((left + right) / 2, (top + bot) / 2);
        }
        
        public Vector DeterminePosition(Vector middle_point)
        {
            float left = Position.X;
            float right = Position.X + Width;
            float top = Position.Y;
            float bot = Position.Y + Height;
            float xCenter = (left + right) / 2;
            float yCenter = (top + bot) / 2;
            Vector LeftAnchor = new Vector(left, yCenter);
            Vector RightAnchor = new Vector(right, yCenter);
            Vector TopAnchor = new Vector(xCenter, top);
            Vector BotAnchor = new Vector(xCenter, bot);
            Vector center = (new Vector((right + left) / 2, (top + bot) / 2) + middle_point) / 2;
            if (center.Y <= bot && center.Y >= top)
            {
                if (center.X > left && center.X > right)
                    return new Vector(right, center.Y);
                else
                    return new Vector(left, center.Y);
            }
            else if (center.X <= right && center.X >= left)
            {
                if (center.Y > top && center.Y > bot)
                    return new Vector(center.X, bot);
                else
                    return new Vector(center.X, top);
            }
            else
            {
                if (middle_point.X < left && middle_point.Y < top)
                {
                    return LeftAnchor;
                }
                else if (middle_point.X > right && middle_point.Y > bot)
                {
                    return RightAnchor;
                }
                else if (middle_point.X > right && middle_point.Y < top)
                {
                    return TopAnchor;
                }
                else
                {
                    return BotAnchor;
                }
            }
        }
    }
}
