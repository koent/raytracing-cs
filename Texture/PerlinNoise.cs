using Raytracer.Vector;

namespace Raytracer.Texture;

public class PerlinNoise : ITexture
{
    private ITexture First, Second;

    private double Scale;

    private Perlin Noise = new Perlin();

    public PerlinNoise(double scale = 1.0) : this(Color.Black, Color.White, scale) { }

    public PerlinNoise(Color first, Color second, double scale = 1.0) : this(new SolidColor(first), new SolidColor(second), scale) { }

    public PerlinNoise(ITexture first, ITexture second, double scale = 1.0)
    {
        First = first;
        Second = second;
        Scale = scale;
    }

    public Color Value(double u, double v, Point3 point) => Color.Lerp(First.Value(u, v, point), Second.Value(u, v, point), Noise.Noise(Scale * point));
}