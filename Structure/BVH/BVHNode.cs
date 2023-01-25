using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure.BVH;

public class BVHNode : IStructure
{
    public IStructure Left, Right;

    public BoundingBox Box;

    public BVHNode(StructureList list, double timeFrom, double timeTo) : this(list.AsArray, timeFrom, timeTo, 0) { }

    private BVHNode(IStructure[] structures, double timeFrom, double timeTo, int depth)
    {
        Func<Point3, double> axis = depth % 3 == 0 ? p => p.X : depth % 3 == 1 ? p => p.Y : p => p.Z;
        Func<IStructure, double> key = s => BVHNode.BoxKey(s, axis);

        switch (structures.Count())
        {
            case 1:
                Left = Right = structures[0];
                break;
            case 2:
                var firstSmaller = key(structures[0]) < key(structures[1]);
                Left = firstSmaller ? structures[0] : structures[1];
                Right = firstSmaller ? structures[1] : structures[0];
                break;
            default:
                var sorted = structures.OrderBy(key);
                int mid = structures.Length / 2;
                Left = new BVHNode(structures[..mid].ToArray(), timeFrom, timeTo, depth + 1);
                Right = new BVHNode(structures[mid..].ToArray(), timeFrom, timeTo, depth + 1);
                break;
        }

        var box = Left.BoundingBox(timeFrom, timeTo) + Right.BoundingBox(timeFrom, timeTo);
        if (!box.HasValue)
            throw new ArgumentException("No bounding box in BVHNode constructor");

        Box = box.Value;
    }

    public bool Hit(Ray incoming, double tMin, HitRecord hitRecord)
    {
        if (!Box.Hit(incoming, tMin, hitRecord.T))
            return false;

        bool leftHit = Left.Hit(incoming, tMin, hitRecord);
        bool rightHit = Right.Hit(incoming, tMin, hitRecord);
        return leftHit || rightHit;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => Box;

    private static double BoxKey(IStructure structure, Func<Point3, double> axis)
    {
        var box = structure.BoundingBox(0, 0);
        if (!box.HasValue)
            throw new ArgumentException("No bounding box in BVHNode constructor");

        return axis(box.Value.Minimum);
    }
}