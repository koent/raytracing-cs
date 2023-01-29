using Raytracer.Vector;

namespace Raytracer.Texture;

public class SolidColor : ITexture
{
    private Color Color;

    public SolidColor(Color color) => Color = color;

    public Color Value(double u, double v, Point3 point) => Color;
}