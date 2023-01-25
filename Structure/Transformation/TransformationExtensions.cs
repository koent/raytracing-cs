using Raytracer.Vector;

namespace Raytracer.Structure.Transformation;

public static class TransofrmationExtensions
{
    public static IStructure Translate(this IStructure structure, Vec3 displacement) => new Translation(structure, displacement);

    public static IStructure TranslateX(this IStructure structure, double displacement) => new Translation(structure, new Vec3(displacement, 0, 0));

    public static IStructure TranslateY(this IStructure structure, double displacement) => new Translation(structure, new Vec3(0, displacement, 0));

    public static IStructure TranslateZ(this IStructure structure, double displacement) => new Translation(structure, new Vec3(0, 0, displacement));

    public static IStructure RotateY(this IStructure structure, double angle) => new YRotation(structure, angle);
}