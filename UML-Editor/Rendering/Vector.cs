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

        public static Vector Zero = new Vector(0, 0);

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }
        public static Vector operator *(Vector left, Vector right)
        {
            return new Vector(left.X * right.X, left.Y * right.Y);
        }
        public static Vector operator /(Vector left, Vector right)
        {
            return new Vector(left.X / right.X, left.Y / right.Y);
        }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.X, v.Y);
        }
        public static implicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y);
        }

        public static implicit operator PointF(Vector v)
        {
            return new PointF(v.X, v.Y);
        }
        public static implicit operator Vector(PointF p)
        {
            return new Vector((int)p.X, (int)p.Y);
        }
        public static Vector operator +(Vector left, int right)
        {
            return new Vector(left.X + right, left.Y + right);
        }
        public static Vector operator -(Vector left, int right)
        {
            return new Vector(left.X - right, left.Y - right);
        }
        public static Vector operator *(Vector left, int right)
        {
            return new Vector(left.X * right, left.Y * right);
        }
        public static Vector operator /(Vector left, int right)
        {
            return new Vector(left.X / right, left.Y / right);
        }
        public static int GetDistance(Vector v)
        {
            return (int)Math.Sqrt((int)Math.Pow((double)v.X, 2) + (int)Math.Pow((double)v.Y, 2));
        }
    }
}
