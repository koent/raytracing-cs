using Raytracer.Vector;

namespace Raytracer.Texture;

public class PerlinTurbulence : ITexture
{
    private ITexture First, Second;

    private double Scale;

    private int Depth;

    private Perlin Noise = new Perlin();

    public PerlinTurbulence(double scale = 1.0, int depth = 7) : this(Color.Black, Color.White, scale, depth) { }

    public PerlinTurbulence(Color first, Color second, double scale = 1.0, int depth = 7) : this(new SolidColor(first), new SolidColor(second), scale, depth) { }

    public PerlinTurbulence(ITexture first, ITexture second, double scale = 1.0, int depth = 7)
    {
        First = first;
        Second = second;
        Scale = scale;
        Depth = depth;
    }

    public Color Value(double u, double v, Point3 point) => Color.Lerp(First.Value(u, v, point), Second.Value(u, v, point), Noise.Turbulence(Scale * point, Depth));
}