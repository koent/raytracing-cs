using Raytracer.Raytracing;
using Raytracer.Vector;
using Raytracer.Structure;

namespace Raytracer.Material;

public interface IMaterial
{
    public abstract ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord);
}