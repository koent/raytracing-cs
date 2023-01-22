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

    public bool Hit(Ray ray, double tMin, double tMax, out HitRecord hitRecord)
    {
        hitRecord = default;
        HitRecord currentRecord;
        bool hitAnything = false;
        var tClosest = tMax;
        foreach (var structure in Structures)
        {
            if (structure.Hit(ray, tMin, tClosest, out currentRecord))
            {
                hitAnything = true;
                tClosest = currentRecord.T;
                hitRecord = currentRecord;
            }
        }

        return hitAnything;
    }

    public BoundingBox? BoundingBox(double timeFrom, double timeTo) => Structures.Select(s => s.BoundingBox(timeFrom, timeTo)).Aggregate((l, r) => l + r);
}