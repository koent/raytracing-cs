using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public interface IStructure
{
    public HitRecord? Hit(Ray ray, double tMin, double tMax, HitRecord hitRecord = default);

    public BoundingBox? BoundingBox(double timeFrom, double timeTo);
}