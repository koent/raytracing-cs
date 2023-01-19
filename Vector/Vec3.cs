using Raytracer.Helpers;

namespace Raytracer.Vector;

public struct Vec3
{
    public double X;

    public double Y;

    public double Z;

    private static double Epsilon = 1E-8;

    public Vec3() : this(0, 0, 0) { }

    public Vec3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vec3(Point3 from, Point3 to)
    {
        X = to.X - from.X;
        Y = to.Y - from.Y;
        Z = to.Z - from.Z;
    }

    public double LengthSquared => X * X + Y * Y + Z * Z;

    public double Length => Math.Sqrt(LengthSquared);

    public Vec3 UnitVector => this / Length;

    public bool IsNearZero => LengthSquared < Epsilon;

    public Vec3 Reflection(Vec3 normal) => this - 2 * Dot(this, normal) * normal;

    public Vec3 Refraction(Vec3 normal, double refractionRatio)
    {
        var cosIncoming = Math.Min(Dot(-this, normal), 1.0);
        var perpendicular = refractionRatio * (this + cosIncoming * normal);
        var parallel = -Math.Sqrt(Math.Abs(1.0 - perpendicular.LengthSquared)) * normal;
        return perpendicular + parallel;
    }

    public override string ToString() => $"{X} {Y} {Z}";

    public static Vec3 Random() => new Vec3(RandomHelper.Instance.NextDouble(), RandomHelper.Instance.NextDouble(), RandomHelper.Instance.NextDouble());

    public static Vec3 Random(double min, double max) => new Vec3(RandomHelper.Instance.NextDouble(min, max), RandomHelper.Instance.NextDouble(min, max), RandomHelper.Instance.NextDouble(min, max));

    public static Vec3 RandomInUnitSphere()
    {
        while (true)
        {
            var vector = Random(-1.0, 1.0);
            if (vector.LengthSquared < 1.0)
                return vector;
        }
    }

    public static Vec3 RandomUnitVector() => RandomInUnitSphere().UnitVector;

    public static Vec3 RandomInUnitHemisphere(Vec3 normal)
    {
        Vec3 inUnitSphere = RandomInUnitSphere();
        return Dot(inUnitSphere, normal) > 0.0 ? inUnitSphere : -inUnitSphere;
    }

    public static Vec3 RandomInUnitDisk()
    {
        while (true)
        {
            var vector = new Vec3(RandomHelper.Instance.NextDouble(-1.0, 1.0), RandomHelper.Instance.NextDouble(-1.0, 1.0), 0.0);
            if (vector.LengthSquared < 1.0)
                return vector;
        }
    }

    // TODO: Add, Mult, ... zonder constructor

    public static Vec3 operator -(Vec3 vector) => new Vec3(-vector.X, -vector.Y, -vector.Z);

    public static Vec3 operator +(Vec3 left, Vec3 right) => new Vec3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Vec3 operator -(Vec3 left, Vec3 right) => left + -right;

    public static Vec3 operator *(double scalar, Vec3 vector) => new Vec3(scalar * vector.X, scalar * vector.Y, scalar * vector.Z);

    public static Vec3 operator *(Vec3 vector, double scalar) => scalar * vector;

    public static Vec3 operator /(Vec3 vector, double denominator) => 1 / denominator * vector;

    public static double Dot(Vec3 left, Vec3 right) => left.X * right.X + left.Y * right.Y + left.Z * right.Z;

    public static Vec3 Cross(Vec3 left, Vec3 right) => new Vec3(
            left.Y * right.Z - left.Z * right.Y,
            left.Z * right.X - left.X * right.Z,
            left.X * right.Y - left.Y * right.X
        );

    public static Vec3 Up => new Vec3(0.0, 1.0, 0.0);
}