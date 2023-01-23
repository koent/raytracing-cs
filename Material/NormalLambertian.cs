using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public class NormalLambertian : IMaterial
{
    public ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord)
    {
        var scatterDirection = hitRecord.Normal + Vec3.RandomUnitVector();

        // Scatter zero edge case
        if (scatterDirection.IsNearZero)
            scatterDirection = hitRecord.Normal;

        var scattered = new Ray(hitRecord.Point, scatterDirection, incoming.Time);
        var attenuation = new Color(0.5 * hitRecord.Normal.X + 0.5, 0.5 * hitRecord.Normal.Y + 0.5, 0.5 * hitRecord.Normal.Z + 0.5);
        return new ScatterRecord(attenuation, scattered);
    }
}