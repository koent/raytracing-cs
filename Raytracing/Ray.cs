using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class Ray
{
    public Point3 Origin;

    public Vec3 Direction;

    public Ray(Point3 origin, Vec3 direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Point3 At(double position) => Origin + position * Direction;
}