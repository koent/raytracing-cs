using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public interface IStructure
{
    public bool Hit(Ray ray, double tMin, double tMax, out HitRecord hitRecord);

    public BoundingBox? BoundingBox(double timeFrom, double timeTo);
}