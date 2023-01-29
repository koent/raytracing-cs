using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public interface IStructure
{
    public bool Hit(Ray ray, HitRecord hitRecord);

    public BoundingBox? BoundingBox();
}