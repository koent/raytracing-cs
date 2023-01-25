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

    public bool Hit(Ray incoming, HitRecord hitRecord)
    {
        var movedRay = new Ray(incoming.Origin - Displacement, incoming.Direction, incoming.Time);
        if (!Structure.Hit(movedRay, hitRecord))
            return false;

        hitRecord.Point += Displacement;
        hitRecord.SetFaceNormal(movedRay, hitRecord.Normal);
        return true;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo)
    {
        var innerBox = Structure.BoundingBox(timeFrom, timeTo);
        if (!innerBox.HasValue)
            return null;

        return new BoundingBox(innerBox.Value.Minimum + Displacement, innerBox.Value.Maximum + Displacement);
    }
}