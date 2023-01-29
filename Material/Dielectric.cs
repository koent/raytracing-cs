using Raytracer.Helpers;
using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Dielectric : IMaterial
{
    public double RefractiveIndex;

    public Dielectric(double refractiveIndex) => RefractiveIndex = refractiveIndex;

    public ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord)
    {
        var refractionRatio = hitRecord.FrontFace ? 1.0 / RefractiveIndex : RefractiveIndex;
        var unitDirection = incoming.Direction.UnitVector;
        var cosIncoming = Math.Min(Vec3.Dot(-unitDirection, hitRecord.Normal), 1.0);
        var sinIncoming = Math.Sqrt(1.0 - cosIncoming * cosIncoming);

        Vec3 direction;
        if (refractionRatio * sinIncoming > 1.0 || Reflectance(cosIncoming, refractionRatio) > RandomHelper.Instance.NextDouble())
            direction = unitDirection.Reflection(hitRecord.Normal);
        else
            direction = unitDirection.Refraction(hitRecord.Normal, refractionRatio);
        var scattered = new Ray(hitRecord.Point, direction);
        return new ScatterRecord(Color.White, scattered);
    }

    private static double Reflectance(double cosine, double refractionRatio)
    {
        var r0 = (1.0 - refractionRatio) / (1.0 + refractionRatio);
        r0 *= r0;
        return r0 + (1.0 - r0) * Math.Pow(1 - cosine, 5);
    }
}