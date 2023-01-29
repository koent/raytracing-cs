using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

public class StructureList : IStructure
{
    private List<IStructure> Structures;

    public StructureList() => Structures = new List<IStructure>();

    public StructureList(IStructure structure) => Structures = new List<IStructure> { structure };

    public void Clear() => Structures.Clear();

    public void Add(IStructure structure) => Structures.Add(structure);

    public bool Hit(Ray ray, HitRecord hitRecord)
    {
        var result = false;
        foreach (var structure in Structures)
            result |= structure.Hit(ray, hitRecord);

        return result;
    }

    public BoundingBox? BoundingBox() => Structures.Select(s => s.BoundingBox()).Aggregate((l, r) => l + r);

    public IStructure[] AsArray => Structures.ToArray();
}