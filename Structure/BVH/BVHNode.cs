using Raytracer.Helpers;
using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Structure.BVH;

public class BVHNode : IStructure
{
    public IStructure Left, Right;

    public BoundingBox Box;

    public BVHNode(StructureList list) : this(list.AsArray, 0) { }

    private BVHNode(IStructure[] structures, int depth)
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
                var mid = structures.Length / 2;
                Left = new BVHNode(structures[..mid].ToArray(), depth + 1);
                Right = new BVHNode(structures[mid..].ToArray(), depth + 1);
                break;
        }

        var box = Left.BoundingBox() + Right.BoundingBox();
        if (!box.HasValue)
            throw new ArgumentException("No bounding box in BVHNode constructor");

        Box = box.Value;
    }

    public bool Hit(Ray incoming, HitRecord hitRecord)
    {
        if (!Box.Hit(incoming, HelperFunctions.TMin, hitRecord.T))
            return false;

        var leftHit = Left.Hit(incoming, hitRecord);
        var rightHit = Right.Hit(incoming, hitRecord);
        return leftHit || rightHit;
    }

    public BoundingBox? BoundingBox() => Box;

    private static double BoxKey(IStructure structure, Func<Point3, double> axis)
    {
        var box = structure.BoundingBox();
        if (!box.HasValue)
            throw new ArgumentException("No bounding box in BVHNode constructor");

        return axis(box.Value.Minimum);
    }
}