using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class HitRecord
{
    public double T;

    public Point3 Point;

    public double U, V;

    public Vec3 Normal;

    public bool FrontFace;

    public IMaterial Material;

    public HitRecord()
    {
        T = double.PositiveInfinity;
        Point = default;
        Normal = default;
        FrontFace = default;
        Material = new Dielectric(1);
        U = default;
        V = default;
    }

    public void Update(Ray ray, double intersection, Func<Point3, Vec3> normalCalculation, Func<Vec3, (double, double)> uv, IMaterial material)
    {
        if (intersection > T)
            throw new Exception($"{nameof(HitRecord)} updated with non-improved hit");

        T = intersection;
        Point = ray.At(intersection);
        var outwardNormal = normalCalculation(Point);
        FrontFace = Vec3.Dot(ray.Direction, outwardNormal) < 0.0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
        (U, V) = uv(outwardNormal);
        Material = material;
    }

    public void SetFaceNormal(Ray ray, Vec3 outwardNormal)
    {
        FrontFace = Vec3.Dot(ray.Direction, outwardNormal) < 0.0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
    }
}