using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure.Transformation;

public class Translation : IStructure
{
    private IStructure Structure;

    private Vec3 Displacement;

    public Translation(IStructure structure, Vec3 displacement)
    {
        Structure = structure;
        Displacement = displacement;
    }

    public HitRecord Hit(Ray incoming, double tMin, HitRecord previousHitRecord)
    {
        var movedRay = new Ray(incoming.Origin - Displacement, incoming.Direction, incoming.Time);
        var hitRecord = Structure.Hit(movedRay, tMin, previousHitRecord);
        // Ugly hack to check whether the previous call improved
        if (previousHitRecord.T == hitRecord.T)
            return previousHitRecord;

        hitRecord.Point += Displacement;
        hitRecord.SetFaceNormal(movedRay, hitRecord.Normal);
        return hitRecord;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo)
    {
        var innerBox = Structure.BoundingBox(timeFrom, timeTo);
        if (!innerBox.HasValue)
            return null;

        return new BoundingBox(innerBox.Value.Minimum + Displacement, innerBox.Value.Maximum + Displacement);
    }
}