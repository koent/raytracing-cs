using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class Box : IStructure
{
    private Point3 Minimum;

    private Point3 Maximum;

    private StructureList Sides;

    public Box(Point3 minimum, Point3 maximum, IMaterial material)
    {
        Minimum = minimum;
        Maximum = maximum;
        Sides = new StructureList();
        Sides.Add(new XYRectangle(minimum.X, maximum.X, minimum.Y, maximum.Y, maximum.Z, material));
        Sides.Add(new XYRectangle(minimum.X, maximum.X, minimum.Y, maximum.Y, minimum.Z, material));
        Sides.Add(new XZRectangle(minimum.X, maximum.X, minimum.Z, maximum.Z, maximum.Y, material));
        Sides.Add(new XZRectangle(minimum.X, maximum.X, minimum.Z, maximum.Z, minimum.Y, material));
        Sides.Add(new YZRectangle(minimum.Y, maximum.Y, minimum.Z, maximum.Z, maximum.X, material));
        Sides.Add(new YZRectangle(minimum.Y, maximum.Y, minimum.Z, maximum.Z, minimum.X, material));
    }

    public bool Hit(Ray ray, HitRecord hitRecord) => Sides.Hit(ray, hitRecord);

    public BoundingBox? BoundingBox() => new BoundingBox(Minimum, Maximum);
}