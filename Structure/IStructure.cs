using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public interface IStructure
{
    public HitRecord Hit(Ray ray, double tMin, HitRecord previousHitRecord);

    public BoundingBox? BoundingBox(double timeFrom, double timeTo);
}