using Raytracer.Helpers;
using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure.BVH;
using Raytracer.Texture;
using Raytracer.Vector;

namespace Raytracer.Structure;

public class ConstantDensityMedium : IStructure
{
    private IStructure Boundary;

    private IMaterial PhaseFunction;

    private double NegativeInverseDensity;

    public ConstantDensityMedium(IStructure boundary, double density, Color color) : this(boundary, density, new SolidColor(color)) { }

    public ConstantDensityMedium(IStructure boundary, double density, ITexture texture)
    {
        Boundary = boundary;
        NegativeInverseDensity = -1.0 / density;
        PhaseFunction = new Isotropic(texture);
    }

    public BoundingBox? BoundingBox() => Boundary.BoundingBox();

    public bool Hit(Ray ray, HitRecord hitRecord)
    {
        var firstHitRecord = new HitRecord();
        if (!Boundary.Hit(ray, firstHitRecord))
            // Ray not hitting object
            return false;

        var tUntilHit = NegativeInverseDensity * Math.Log(RandomHelper.Instance.NextDouble()) / ray.Direction.Length;

        double tStart = 0;
        var tInBoundary = firstHitRecord.T;
        var secondHitRecord = new HitRecord();
        if (Boundary.Hit(new Ray(firstHitRecord.Point, ray.Direction), secondHitRecord))
        {
            // Ray hitting object, starting before object
            // Otherwise: ray started inside object
            tStart = firstHitRecord.T;
            tInBoundary = secondHitRecord.T;
        }

        if (tInBoundary < tUntilHit)
            return false;

        if (hitRecord.T < tStart + tUntilHit)
            return false;

        hitRecord.Update(ray, tStart + tUntilHit, _ => default, _ => default, PhaseFunction);
        return true;
    }
}