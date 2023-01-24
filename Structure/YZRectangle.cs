using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class YZRectangle : IStructure
{
    public IMaterial Material;

    public double MinY, MaxY;

    public double MinZ, MaxZ;

    public double X;

    public YZRectangle(double minY, double maxY, double minZ, double maxZ, double x, IMaterial material)
    {
        MinY = minY;
        MaxY = maxY;
        MinZ = minZ;
        MaxZ = maxZ;
        X = x;
        Material = material;
    }

    public HitRecord Hit(Ray ray, double tMin, HitRecord previousHitRecord)
    {
        var t = (X - ray.Origin.X) / ray.Direction.X;
        if (t < tMin || previousHitRecord.T < t)
            return previousHitRecord;

        var y = ray.Origin.Y + t * ray.Direction.Y;
        var z = ray.Origin.Z + t * ray.Direction.Z;
        if (y < MinY || MaxY < y || z < MinZ || MaxZ < z)
            return previousHitRecord;

        return new HitRecord(ray, t, p => new Vec3(1, 0, 1), UV, Material);
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => new BoundingBox(new Point3(X - 0.0001, MinY, MinZ), new Point3(X + 0.0001, MaxY, MaxZ));

    private (double, double) UV(Vec3 vector) => ((vector.Y - MinY) / (MaxY - MinY), (vector.Z - MinZ) / (MaxZ - MinZ));
}