using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class Ray
{
    public Point3 Origin;

    public Vec3 Direction;

    public double Time;

    public Ray(Point3 origin, Vec3 direction, double time = 0.0)
    {
        Origin = origin;
        Direction = direction;
        Time = time;
    }

    public Point3 At(double position) => Origin + position * Direction;
}