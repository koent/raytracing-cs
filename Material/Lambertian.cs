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

    public bool Scatter(Ray incoming, HitRecord hitRecord, out Color attenuation, out Ray scattered)
    {
        var scatterDirection = hitRecord.Normal + Vec3.RandomUnitVector();

        // Scatter zero edge case
        if (scatterDirection.IsNearZero)
            scatterDirection = hitRecord.Normal;

        scattered = new Ray(hitRecord.Point, scatterDirection, incoming.Time);
        attenuation = Albedo.Value(hitRecord.U, hitRecord.V, hitRecord.Point);
        return true;
    }
}