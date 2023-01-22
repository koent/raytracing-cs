using Raytracer.Vector;

namespace Raytracer.Texture;

public interface ITexture
{
    public Color Value(double u, double v, Point3 point);
}