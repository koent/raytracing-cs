using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public class StructureList : IStructure
{
    private List<IStructure> Structures;

    public StructureList()
    {
        Structures = new List<IStructure>();
    }

    public StructureList(IStructure structure)
    {
        Structures = new List<IStructure> { structure };
    }

    public void Clear() => Structures.Clear();

    public void Add(IStructure structure) => Structures.Add(structure);

    public HitRecord Hit(Ray ray, double tMin, HitRecord previousHitRecord)
    {
        var result = previousHitRecord;
        foreach (var structure in Structures)
            result = structure.Hit(ray, tMin, result);

        return result;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => Structures.Select(s => s.BoundingBox(timeFrom, timeTo)).Aggregate((l, r) => l + r);

    public IStructure[] AsArray => Structures.ToArray();
}