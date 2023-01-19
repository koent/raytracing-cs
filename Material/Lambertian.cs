using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Lambertian : IMaterial
{
    public Color Albedo;

    public Lambertian(Color color)
    {
        Albedo = color;
    }

    public bool Scatter(Ray incoming, HitRecord hitRecord, out Color attenuation, out Ray scattered)
    {
        var scatterDirection = hitRecord.Normal + Vec3.RandomUnitVector();

        // Scatter zero edge case
        if (scatterDirection.IsNearZero)
            scatterDirection = hitRecord.Normal;

        scattered = new Ray(hitRecord.Point, scatterDirection);
        attenuation = Albedo;
        return true;
    }
}