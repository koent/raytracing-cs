namespace Raytracer.Vector;

public struct Point3
{
    public double X;

    public double Y;

    public double Z;

    public Point3() : this(0, 0, 0) { }

    public Point3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() => $"{X} {Y} {Z}";

    public static Point3 operator +(Point3 left, Vec3 right) => new Point3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Point3 operator -(Point3 left, Vec3 right) => left + -right;
}