namespace Raytracer.Helpers;

public static class RandomHelper
{
    private static Random _instance = new Random();

    public static Random Instance => _instance;
}