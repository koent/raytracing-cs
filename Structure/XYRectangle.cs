using Raytracer.Helpers;
using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class XYRectangle : IStructure
{
    public IMaterial Material;

    public double MinX, MaxX;

    public double MinY, MaxY;

    public double Z;

    public XYRectangle(double minX, double maxX, double minY, double maxY, double z, IMaterial material)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        Z = z;
        Material = material;
    }

    public bool Hit(Ray ray, HitRecord hitRecord)
    {
        var t = (Z - ray.Origin.Z) / ray.Direction.Z;
        if (t < HelperFunctions.TMin || hitRecord.T < t)
            return false;

        var x = ray.Origin.X + t * ray.Direction.X;
        var y = ray.Origin.Y + t * ray.Direction.Y;
        if (x < MinX || MaxX < x || y < MinY || MaxY < y)
            return false;

        hitRecord.Update(ray, t, p => new Vec3(0, 0, 1), UV, Material);
        return true;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => new BoundingBox(new Point3(MinX, MinY, Z - 0.0001), new Point3(MaxX, MaxY, Z + 0.0001));

    private (double, double) UV(Vec3 vector) => ((vector.X - MinX) / (MaxX - MinX), (vector.Y - MinY) / (MaxY - MinY));
}