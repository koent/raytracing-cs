using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class CameraSettings
{
    public Point3 LookFrom, LookAt;

    public double FieldOfView;

    public double Aperture;

    public double FocusDistance;

    public CameraSettings(Point3 lookFrom, Point3 lookAt, double fieldOfView, double aperture)
    {
        LookFrom = lookFrom;
        LookAt = lookAt;
        FieldOfView = fieldOfView;
        Aperture = aperture;
        FocusDistance = new Vec3(lookFrom, lookAt).Length;
    }

    public CameraSettings(Point3 lookFrom, Point3 lookAt, double fieldOfView, double aperture, double focusDistance)
    {
        LookFrom = lookFrom;
        LookAt = lookAt;
        FieldOfView = fieldOfView;
        Aperture = aperture;
        FocusDistance = focusDistance;
    }
}