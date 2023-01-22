using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure.BVH;

public struct BoundingBox
{
    public Point3 Minimum, Maximum;

    public BoundingBox(Point3 minimum, Point3 maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    public bool Hit(Ray incoming, double tMin, double tMax) => OverlapCheck(p => p.X, incoming, ref tMin, ref tMax)
                                                            && OverlapCheck(p => p.Y, incoming, ref tMin, ref tMax)
                                                            && OverlapCheck(p => p.Z, incoming, ref tMin, ref tMax);

    private bool SimpleOverlapCheck(Func<Point3, double> axis, Ray incoming, ref double tMin, ref double tMax)
    {
        var tFirst = Math.Min((axis(Minimum) - axis(incoming.Origin)) / axis(incoming.Direction.AsPoint3),
                              (axis(Maximum) - axis(incoming.Origin)) / axis(incoming.Direction.AsPoint3));
        var tSecond = Math.Max((axis(Minimum) - axis(incoming.Origin)) / axis(incoming.Direction.AsPoint3),
                               (axis(Maximum) - axis(incoming.Origin)) / axis(incoming.Direction.AsPoint3));
        tMin = Math.Max(tMin, tFirst);
        tMax = Math.Min(tMax, tSecond);
        return tMin < tMax;
    }

    private bool OverlapCheck(Func<Vec3, double> axis, Ray incoming, ref double tMin, ref double tMax)
    {
        var inverseDirection = 1.0 / axis(incoming.Direction);
        var tFirst = axis(new Vec3(incoming.Origin, Minimum)) * inverseDirection;
        var tSecond = axis(new Vec3(incoming.Origin, Maximum)) * inverseDirection;
        if (inverseDirection < 0.0)
            (tFirst, tSecond) = (tSecond, tFirst);
        tMin = tFirst > tMin ? tFirst : tMin;
        tMax = tSecond < tMax ? tSecond : tMax;
        return tMin < tMax;
    }

    public static BoundingBox? operator +(BoundingBox? left, BoundingBox? right)
    {
        if (!left.HasValue)
            return right;
        if (!right.HasValue)
            return left;

        var minimum = new Point3(Math.Min(left.Value.Minimum.X, right.Value.Minimum.X), Math.Min(left.Value.Minimum.Y, right.Value.Minimum.Y), Math.Min(left.Value.Minimum.Z, right.Value.Minimum.Z));
        var maximum = new Point3(Math.Max(left.Value.Maximum.X, right.Value.Maximum.X), Math.Max(left.Value.Maximum.Y, right.Value.Maximum.Y), Math.Max(left.Value.Maximum.Z, right.Value.Maximum.Z));
        return new BoundingBox(minimum, maximum);
    }
}