using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public interface IMaterial
{
    public abstract ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord);

    public Color Emitted(double u, double v, Point3 point) => Color.Black;
}