using Raytracer.Helpers;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Vector;

namespace Raytracer.Structure.Transformation;

public class YRotation : IStructure
{
    private IStructure Structure;

    private double Sinθ, Cosθ;

    private BoundingBox? OuterBoundingBox;

    public YRotation(IStructure structure, double angle)
    {
        Structure = structure;
        OuterBoundingBox = null;
        var θ = HelperFunctions.DegreesToRadians(angle);
        Sinθ = Math.Sin(θ);
        Cosθ = Math.Cos(θ);
        var innerBoundingBox = Structure.BoundingBox();
        if (!innerBoundingBox.HasValue)
            return;

        var minimum = new Point3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        var maximum = new Point3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        var box = innerBoundingBox.Value;

        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                for (var k = 0; k < 2; k++)
                {
                    var x = i * box.Maximum.X + (1 - i) * box.Minimum.X;
                    var y = j * box.Maximum.Y + (1 - j) * box.Minimum.Y;
                    var z = k * box.Maximum.Z + (1 - k) * box.Minimum.Z;
                    var newX = Cosθ * x + Sinθ * z;
                    var newZ = -Sinθ * x + Cosθ * z;
                    var tester = new Vec3(newX, y, newZ);
                    minimum = new Point3(Math.Min(minimum.X, tester.X), Math.Min(minimum.Y, tester.Y), Math.Min(minimum.Z, tester.Z));
                    maximum = new Point3(Math.Max(maximum.X, tester.X), Math.Max(maximum.Y, tester.Y), Math.Max(maximum.Z, tester.Z));
                }
            }
        }
        OuterBoundingBox = new BoundingBox(minimum, maximum);
    }

    public bool Hit(Ray ray, HitRecord hitRecord)
    {
        var rotatedOrigin = new Point3(Cosθ * ray.Origin.X - Sinθ * ray.Origin.Z, ray.Origin.Y, Sinθ * ray.Origin.X + Cosθ * ray.Origin.Z);
        var rotatedDirection = new Vec3(Cosθ * ray.Direction.X - Sinθ * ray.Direction.Z, ray.Direction.Y, Sinθ * ray.Direction.X + Cosθ * ray.Direction.Z);
        var rotatedRay = new Ray(rotatedOrigin, rotatedDirection);
        if (!Structure.Hit(rotatedRay, hitRecord))
            return false;

        var hitPoint = new Point3(Cosθ * hitRecord.Point.X + Sinθ * hitRecord.Point.Z, hitRecord.Point.Y, -Sinθ * hitRecord.Point.X + Cosθ * hitRecord.Point.Z);
        var normal = new Vec3(Cosθ * hitRecord.Normal.X + Sinθ * hitRecord.Normal.Z, hitRecord.Normal.Y, -Sinθ * hitRecord.Normal.X + Cosθ * hitRecord.Normal.Z);

        hitRecord.Point = hitPoint;
        hitRecord.SetFaceNormal(rotatedRay, normal);
        return true;
    }

    public BoundingBox? BoundingBox() => OuterBoundingBox;
}