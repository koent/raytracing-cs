using Raytracer.Helpers;
using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class Camera
{
    private Point3 Origin;

    private Point3 LowerLeftCorner;

    private Vec3 Horizontal;

    private Vec3 Vertical;

    private Vec3 U, V, W;

    private double LensRadius;

    public Camera(CameraSettings settings, double aspectRatio) : this(settings.LookFrom, settings.LookAt, Vec3.Up, settings.FieldOfView, aspectRatio, settings.Aperture, settings.FocusDistance) { }

    public Camera(Point3 lookFrom, Point3 lookAt, Vec3 up, double fieldOfView, double aspectRatio, double aperture, double focusDistance)
    {
        var theta = HelperFunctions.DegreesToRadians(fieldOfView);
        var h = Math.Tan(theta / 2.0);
        var viewportHeight = 2.0 * h;
        var viewportWidth = aspectRatio * viewportHeight;

        W = -new Vec3(lookFrom, lookAt).UnitVector;
        U = Vec3.Cross(up, W).UnitVector;
        V = Vec3.Cross(W, U);

        Origin = lookFrom;
        Horizontal = focusDistance * viewportWidth * U;
        Vertical = focusDistance * viewportHeight * V;
        LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - focusDistance * W;

        LensRadius = aperture / 2.0;
    }

    public Ray GetRay(double u, double v)
    {
        var standardOffset = LensRadius * Vec3.RandomInUnitDisk();
        var offset = U * standardOffset.X + U * standardOffset.Y;
        var direction = new Vec3(Origin + offset, LowerLeftCorner + u * Horizontal + v * Vertical);
        return new Ray(Origin + offset, direction);
    }
}