using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure;

public struct HitRecord
{
    public double T;

    public Point3 Point;

    public Vec3 Normal;

    public bool FrontFace;

    public IMaterial Material;

    public bool IsHit;

    public HitRecord()
    {
        T = double.PositiveInfinity;
        IsHit = false;

        Point = default;
        Normal = default;
        FrontFace = default;
        Material = new Dielectric(1);
    }

    public HitRecord(Ray ray, double intersection, Func<Point3, Vec3> normalCalculation, IMaterial material)
    {
        T = intersection;
        Point = ray.At(intersection);
        var outwardNormal = normalCalculation(Point);
        FrontFace = Vec3.Dot(ray.Direction, outwardNormal) < 0.0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
        Material = material;
        IsHit = true;
    }
}