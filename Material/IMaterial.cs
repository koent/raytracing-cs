using Raytracer.Raytracing;
using Raytracer.Vector;
using Raytracer.Structure;

namespace Raytracer.Material;

public interface IMaterial
{
    public abstract bool Scatter(Ray incoming, HitRecord hitRecord, out Color attenuation, out Ray scattered);
}