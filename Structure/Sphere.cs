using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
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

    public HitRecord Hit(Ray ray, double tMin, HitRecord previousHitRecord)
    {
        var centerToOrigin = new Vec3(Center, ray.Origin);
        var a = ray.Direction.LengthSquared;
        var half_b = Vec3.Dot(centerToOrigin, ray.Direction);
        var c = centerToOrigin.LengthSquared - Radius * Radius;
        var discriminant = half_b * half_b - a * c;
        if (discriminant < 0)
            return previousHitRecord;

        var rootDiscriminant = Math.Sqrt(discriminant);

        // Find nearest intersection between tMin and tMax
        var intersection = (-half_b - rootDiscriminant) / a;
        if (intersection < tMin || previousHitRecord.T < intersection)
        {
            intersection = (-half_b + rootDiscriminant) / a;
            if (intersection < tMin || previousHitRecord.T < intersection)
                return previousHitRecord;
        }

        return new HitRecord(ray, intersection, Normal, Material);
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => new BoundingBox(Center - new Vec3(Radius, Radius, Radius), Center + new Vec3(Radius, Radius, Radius));
}