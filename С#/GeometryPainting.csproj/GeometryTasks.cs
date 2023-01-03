using System;
using System.Drawing;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Geometry
    {
        static public double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        static public double GetLength(Segment segment)
        {
            return GetLength(new Vector { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y });
        }

        static public Vector Add(Vector vector, Vector newVector)
        {
            return new Vector { X = vector.X + newVector.X, Y = vector.Y + newVector.Y };
        }

        static public bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var cutBegin = new Segment { Begin = vector, End = segment.Begin };
            var cutEnd = new Segment { Begin = vector, End = segment.End };
            return GetLength(cutBegin) + GetLength(cutEnd) == GetLength(segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public Color Color { get; internal set; }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}