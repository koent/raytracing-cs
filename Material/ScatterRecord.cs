using Raytracer.Raytracing;
using Raytracer.Vector;

namespace Raytracer.Material;

public struct ScatterRecord
{
    public Color Attenuation;

    public Ray Scattered;

    public ScatterRecord(Color attenuation, Ray scattered)
    {
        Attenuation = attenuation;
        Scattered = scattered;
    }
}