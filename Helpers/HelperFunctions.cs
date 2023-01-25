namespace Raytracer.Helpers;

public static class HelperFunctions
{
    public static double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;

    public static double NextDouble(this Random random, double min, double max) => min + (max - min) * random.NextDouble();

    public static double TMin = 0.001;
}