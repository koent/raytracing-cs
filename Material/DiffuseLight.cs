using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Texture;
using Raytracer.Vector;

namespace Raytracer.Material;

public class DiffuseLight : IMaterial
{
    private ITexture Emit;

    public DiffuseLight(double strength = 1.0) : this(new SolidColor(strength * Color.White)) { }

    public DiffuseLight(Color color, double strength = 1.0) : this(new SolidColor(strength * color)) { }

    public DiffuseLight(ITexture texture)
    {
        Emit = texture;
    }

    public ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord) => null;

    public Color Emitted(double u, double v, Point3 point) => Emit.Value(u, v, point);
}