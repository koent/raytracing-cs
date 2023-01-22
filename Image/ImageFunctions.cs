using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Image;

public static class ImageFunctions
{

    public static Color NormalShading(Vec3 normal) => new Color(0.5 * (normal.X + 1), 0.5 * (normal.Y + 1), 0.5 * (normal.Z + 1));

    public static Color RayColor(Ray ray, IStructure world, int depth)
    {
        if (depth <= 0)
            return Color.Black;

        var hitRecord = world.Hit(ray, 0.001, double.MaxValue);
        if (hitRecord.HasValue)
        {
            Ray scattered;
            Color attenuation;
            if (hitRecord.Value.Material.Scatter(ray, hitRecord.Value, out attenuation, out scattered))
                return attenuation * RayColor(scattered, world, depth - 1);
            return Color.Black;
        }

        Vec3 unitDirection = ray.Direction.UnitVector;
        var t = 0.5 * (unitDirection.Y + 1.0);
        return Color.Lerp(Color.White, Color.Sky, t);
    }
}