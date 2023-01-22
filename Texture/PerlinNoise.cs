using Raytracer.Vector;

namespace Raytracer.Texture;

public class PerlinNoise : ITexture
{
    private ITexture First, Second;

    private Perlin Noise = new Perlin();

    public PerlinNoise() : this(Color.Black, Color.White) { }

    public PerlinNoise(Color first, Color second) : this(new SolidColor(first), new SolidColor(second)) { }

    public PerlinNoise(ITexture first, ITexture second)
    {
        First = first;
        Second = second;
    }

    public Color Value(double u, double v, Point3 point) => Color.Lerp(First.Value(u, v, point), Second.Value(u, v, point), Noise.Noise(point));
}