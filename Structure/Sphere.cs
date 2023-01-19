using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class Sphere : IStructure
{
    public Point3 Center;

    public double Radius;

    public IMaterial Material;

    public Sphere(Point3 center, double radius, IMaterial material)
    {
        Center = center;
        Radius = radius;
        Material = material;
    }

    public Vec3 Normal(Point3 point) => new Vec3(Center, point) / Radius;

    public bool Hit(Ray ray, double tMin, double tMax, out HitRecord hitRecord)
    {
        hitRecord = default;
        var originToCenter = new Vec3(Center, ray.Origin);
        var a = ray.Direction.LengthSquared;
        var half_b = Vec3.Dot(originToCenter, ray.Direction);
        var c = originToCenter.LengthSquared - Radius * Radius;
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

        hitRecord = new HitRecord(ray, intersection, Normal, Material);
        return true;
    }
}