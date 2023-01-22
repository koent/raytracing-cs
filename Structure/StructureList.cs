using Raytracer.Raytracing;
using Raytracer.Structure.BVH;

namespace Raytracer.Structure;

class StructureList : IStructure
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

    public HitRecord? Hit(Ray ray, double tMin, double tMax, HitRecord _)
    {
        HitRecord? result = null;
        var tClosest = tMax;
        foreach (var structure in Structures)
        {
            var hitRecord = structure.Hit(ray, tMin, tClosest, _);
            if (hitRecord.HasValue)
            {
                tClosest = hitRecord.Value.T;
                result = hitRecord;
            }
        }

        return result;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => Structures.Select(s => s.BoundingBox(timeFrom, timeTo)).Aggregate((l, r) => l + r);
}