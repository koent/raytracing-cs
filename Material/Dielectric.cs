using Raytracer.Helpers;
using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Dielectric : IMaterial
{
    public double RefractiveIndex;

    public Dielectric(double refractiveIndex)
    {
        RefractiveIndex = refractiveIndex;
    }

    public bool Scatter(Ray incoming, HitRecord hitRecord, out Color attenuation, out Ray scattered)
    {
        attenuation = Color.White;
        double refractionRatio = hitRecord.FrontFace ? 1.0 / RefractiveIndex : RefractiveIndex;
        Vec3 unitDirection = incoming.Direction.UnitVector;
        double cosIncoming = Math.Min(Vec3.Dot(-unitDirection, hitRecord.Normal), 1.0);
        double sinIncoming = Math.Sqrt(1.0 - cosIncoming * cosIncoming);

        Vec3 direction;
        if (refractionRatio * sinIncoming > 1.0 || Reflectance(cosIncoming, refractionRatio) > RandomHelper.Instance.NextDouble())
            direction = unitDirection.Reflection(hitRecord.Normal);
        else
            direction = unitDirection.Refraction(hitRecord.Normal, refractionRatio);
        scattered = new Ray(hitRecord.Point, direction, incoming.Time);
        return true;
    }

    private static double Reflectance(double cosine, double refractionRatio)
    {
        var r0 = (1.0 - refractionRatio) / (1.0 + refractionRatio);
        r0 *= r0;
        return r0 + (1.0 - r0) * Math.Pow(1 - cosine, 5);
    }
}