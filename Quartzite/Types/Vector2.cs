using System.Numerics;

namespace Quartzite
{
    public struct Vector(float X, float Y)
    {
        public float x = X;

        public float y = Y;

        public float Length()
        {
            return x + y;
        }

        public float LengthSqrt()
        {
            return (float)MathF.Sqrt(x * x + y * y);
        }

        public float SquaredLength()
        {
            return x * x + y * y;
        }

        public static Vector Normalize(Vector vector)
        {
            Vector result = vector / vector.LengthSqrt();

            if (!float.IsNaN(result.y) && !float.IsNaN(result.y))
                return result;

            return new Vector(0, 0);
        }

        public static Vector RotateAround(Vector position, float rotateBy)
        {
            return new Vector(position.x * (float)MathF.Cos(rotateBy) - position.y * (float)MathF.Sin(rotateBy), position.y * (float)MathF.Cos(rotateBy) + position.x * (float)MathF.Sin(rotateBy)); // l o n g
        }

        public static Vector ClampMagnitude(Vector vector, float length)
        {
            Vector result = Vector.Normalize(vector) * length;
            return result;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, a.y * b.y);
        }

        public static Vector operator *(Vector v, float i)
        {
            return new Vector(v.x * i, v.y * i);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.x / b.x, a.y / b.y);
        }

        public static Vector operator /(Vector v, float i)
        {
            return new Vector(v.x / i, v.y / i);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static implicit operator Vector2(Vector a)
        {
            return new Vector2(a.x, a.y);
        }

        public static implicit operator Vector(Vector2 a)
        {
            return new Vector(a.X, a.Y);
        }

        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public override bool Equals(object? obj) // because Vector class header complained without it
        {
            return obj is Vector v && this == v;
        }

        public override int GetHashCode() // because Vector class header complained without it
        {
            return HashCode.Combine(x, y);
        }
    }
}