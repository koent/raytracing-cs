using Raytracer.Vector;

namespace Raytracer.Texture;

public class Checkerboard : ITexture
{
    private ITexture Even;

    private ITexture Odd;

    public Checkerboard(Color even, Color odd) : this(new SolidColor(even), new SolidColor(odd)) { }

    public Checkerboard(ITexture even, ITexture odd)
    {
        Even = even;
        Odd = odd;
    }

    public Color Value(double u, double v, Point3 point)
    {
        var sines = Math.Sin(10 * point.X) * Math.Sin(10 * point.Y) * Math.Sin(10 * point.Z);
        return sines < 0 ? Odd.Value(u, v, point) : Even.Value(u, v, point);
    }
}