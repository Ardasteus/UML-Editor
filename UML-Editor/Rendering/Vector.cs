using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UML_Editor.Rendering
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(right.X + left.X, right.Y + left.Y);
        }
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(right.X - left.X, right.Y - left.Y);
        }

        public static Vector operator *(Vector left, Vector right)
        {
            return new Vector(right.X * left.X, right.Y * left.Y);
        }
        public static Vector operator /(Vector left, Vector right)
        {
            return new Vector(right.X / left.X, right.Y / left.Y);
        }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.X, v.Y);
        }
        public static implicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y);
        }
    }
}
