using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Texture;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Lambertian : IMaterial
{
    public ITexture Albedo;

    public Lambertian(Color color) : this(new SolidColor(color)) { }

    public Lambertian(ITexture texture)
    {
        Albedo = texture;
    }

    public ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord)
    {
        var scatterDirection = hitRecord.Normal + Vec3.RandomUnitVector();

        // Scatter zero edge case
        if (scatterDirection.IsNearZero)
            scatterDirection = hitRecord.Normal;

        var scattered = new Ray(hitRecord.Point, scatterDirection, incoming.Time);
        var attenuation = Albedo.Value(hitRecord.U, hitRecord.V, hitRecord.Point);
        return new ScatterRecord(attenuation, scattered);
    }
}