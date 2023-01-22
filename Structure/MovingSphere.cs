using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class MovingSphere : IStructure
{
    public Point3 CenterFrom, CenterTo;

    public double Radius;

    public IMaterial Material;

    public double TimeFrom, TimeTo;

    public MovingSphere(Point3 centerFrom, Point3 centerTo, double timeFrom, double timeTo, double radius, IMaterial material)
    {
        CenterFrom = centerFrom;
        CenterTo = centerTo;
        TimeFrom = timeFrom;
        TimeTo = timeTo;
        Radius = radius;
        Material = material;
    }

    public Point3 Center(double time) => CenterFrom + new Vec3(CenterFrom, CenterTo) * ((time - TimeFrom) / (TimeTo - TimeFrom));

    public Func<Point3, Vec3> Normal(double time) => (Point3 point) => new Vec3(Center(time), point) / Radius;

    public bool Hit(Ray ray, double tMin, double tMax, out HitRecord hitRecord)
    {
        hitRecord = default;
        var centerToOrigin = new Vec3(Center(ray.Time), ray.Origin);
        var a = ray.Direction.LengthSquared;
        var half_b = Vec3.Dot(centerToOrigin, ray.Direction);
        var c = centerToOrigin.LengthSquared - Radius * Radius;
        var discriminant = half_b * half_b - a * c;
        if (discriminant < 0)
            return false;

        var rootDiscriminant = Math.Sqrt(discriminant);

        // Find nearest intersection between tMin and tMax
        var intersection = (-half_b - rootDiscriminant) / a;
        if (intersection < tMin || tMax < intersection)
        {
            intersection = (-half_b + rootDiscriminant) / a;
            if (intersection < tMin || tMax < intersection)
                return false;
        }

        hitRecord = new HitRecord(ray, intersection, Normal(ray.Time), Material);
        return true;
    }
}