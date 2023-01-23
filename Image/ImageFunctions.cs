using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Image;

public static class ImageFunctions
{

    public static Color NormalShading(Vec3 normal) => new Color(0.5 * (normal.X + 1), 0.5 * (normal.Y + 1), 0.5 * (normal.Z + 1));

    public static Color RayColor(Ray ray, IStructure world, Color background, int depth)
    {
        if (depth <= 0)
            return Color.Black;

        var hitRecord = world.Hit(ray, 0.001, new HitRecord());
        if (!hitRecord.IsHit)
            return background;

        var scatterRecord = hitRecord.Material.Scatter(ray, hitRecord);
        var emitted = hitRecord.Material.Emitted(hitRecord.U, hitRecord.V, hitRecord.Point);
        if (!scatterRecord.HasValue)
            return emitted;

        return emitted + scatterRecord.Value.Attenuation * RayColor(scatterRecord.Value.Scattered, world, background, depth - 1);
    }
}