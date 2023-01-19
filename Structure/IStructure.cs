using Raytracer.Raytracing;

namespace Raytracer.Structure;

public interface IStructure
{
    public bool Hit(Ray ray, double tMin, double tMax, out HitRecord hitRecord);
}