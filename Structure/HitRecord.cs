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

    public HitRecord(Ray ray, double intersection, Func<Point3, Vec3> normalCalculation, IMaterial material)
    {
        T = intersection;
        Point = ray.At(intersection);
        var outwardNormal = normalCalculation(Point);
        FrontFace = Vec3.Dot(ray.Direction, outwardNormal) < 0.0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
        Material = material;
    }
}