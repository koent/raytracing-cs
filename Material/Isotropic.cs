using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Texture;
using Raytracer.Vector;

namespace Raytracer.Material;

public class Isotropic : IMaterial
{
    private ITexture Albedo;

    public Isotropic(ITexture albedo) => Albedo = albedo;

    public ScatterRecord? Scatter(Ray incoming, HitRecord hitRecord)
    {
        var scattered = new Ray(hitRecord.Point, Vec3.RandomInUnitSphere());
        var attenuation = Albedo.Value(hitRecord.V, hitRecord.U, hitRecord.Point);
        return new ScatterRecord(attenuation, scattered);
    }
}