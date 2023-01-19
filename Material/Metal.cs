using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Metal : IMaterial
{
    public Color Albedo;

    public double Fuzziness;

    public Metal(Color color, double fuzziness)
    {
        Albedo = color;
        Fuzziness = Math.Min(fuzziness, 1.0);
    }

    public bool Scatter(Ray incoming, HitRecord hitRecord, out Color attenuation, out Ray scattered)
    {
        var reflected = incoming.Direction.UnitVector.Reflection(hitRecord.Normal);
        scattered = new Ray(hitRecord.Point, reflected + Fuzziness*Vec3.RandomInUnitSphere());
        attenuation = Albedo;
        return Vec3.Dot(scattered.Direction, hitRecord.Normal) > 0.0;
    }
}