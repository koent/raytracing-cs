using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class CameraSettings
{
    public Point3 LookFrom, LookAt;

    public double FieldOfView;

    public double Aperture;

    public double FocusDistance;

    public double TimeFrom, TimeTo;

    public CameraSettings(Point3 lookFrom, Point3 lookAt, double fieldOfView, double aperture, double timeFrom, double timeTo)
    {
        LookFrom = lookFrom;
        LookAt = lookAt;
        FieldOfView = fieldOfView;
        Aperture = aperture;
        FocusDistance = new Vec3(lookFrom, lookAt).Length;
        TimeFrom = timeFrom;
        TimeTo = timeTo;
    }

    public CameraSettings(Point3 lookFrom, Point3 lookAt, double fieldOfView, double aperture, double focusDistance, double timeFrom, double timeTo)
    {
        LookFrom = lookFrom;
        LookAt = lookAt;
        FieldOfView = fieldOfView;
        Aperture = aperture;
        FocusDistance = focusDistance;
        TimeFrom = timeFrom;
        TimeTo = timeTo;
    }


}