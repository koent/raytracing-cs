using Raytracer.Helpers;
using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class XZRectangle : IStructure
{
    public IMaterial Material;

    public double MinX, MaxX;

    public double MinZ, MaxZ;

    public double Y;

    public XZRectangle(double minX, double maxX, double minZ, double maxZ, double y, IMaterial material)
    {
        MinX = minX;
        MaxX = maxX;
        MinZ = minZ;
        MaxZ = maxZ;
        Y = y;
        Material = material;
    }

    public bool Hit(Ray ray, HitRecord hitRecord)
    {
        var t = (Y - ray.Origin.Y) / ray.Direction.Y;
        if (t < HelperFunctions.TMin || hitRecord.T < t)
            return false;

        var x = ray.Origin.X + t * ray.Direction.X;
        var z = ray.Origin.Z + t * ray.Direction.Z;
        if (x < MinX || MaxX < x || z < MinZ || MaxZ < z)
            return false;

        hitRecord.Update(ray, t, p => new Vec3(0, 1, 0), UV, Material);
        return true;
    }

    public BoundingBox? BoundingBox() => new BoundingBox(new Point3(MinX, Y - 0.0001, MinZ), new Point3(MaxX, Y + 0.0001, MaxZ));

    private (double, double) UV(Vec3 vector) => ((vector.X - MinX) / (MaxX - MinX), (vector.Z - MinZ) / (MaxZ - MinZ));
}