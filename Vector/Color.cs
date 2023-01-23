using Raytracer.Helpers;

namespace Raytracer.Vector;

public struct Color
{
    public double R;

    public double G;

    public double B;

    public Color() : this(0, 0, 0) { }

    public Color(double r, double g, double b)
    {
        R = r;
        G = g;
        B = b;
    }

    // public string ToString(int samplesPerPixel) => (this / samplesPerPixel).ToString();

    public static Color Lerp(Color start, Color end, double position) => (1 - position) * start + position * end;

    public static Color Random() => new Color(RandomHelper.Instance.NextDouble(), RandomHelper.Instance.NextDouble(), RandomHelper.Instance.NextDouble());

    public static Color Random(double min, double max) => new Color(RandomHelper.Instance.NextDouble(min, max), RandomHelper.Instance.NextDouble(min, max), RandomHelper.Instance.NextDouble(min, max));

    public void Add(Color right)
    {
        R += right.R;
        G += right.G;
        B += right.B;
    }

    public static Color operator -(Color vector) => new Color(-vector.R, -vector.G, -vector.B);

    public static Color operator +(Color left, Color right) => new Color(left.R + right.R, left.G + right.G, left.B + right.B);

    public static Color operator -(Color left, Color right) => left + -right;

    public static Color operator *(Color left, Color right) => new Color(left.R * right.R, left.G * right.G, left.B * right.B);

    public static Color operator *(double scalar, Color vector) => new Color(scalar * vector.R, scalar * vector.G, scalar * vector.B);

    public static Color operator *(Color vector, double scalar) => scalar * vector;

    public static Color operator /(Color vector, double denominator) => 1 / denominator * vector;

    public static Color White => new Color(1.0, 1.0, 1.0);

    public static Color Black => new Color(0.0, 0.0, 0.0);

    public static Color Sky => new Color(0.5, 0.7, 1.0);

    public static Color LightSky => new Color(0.7, 0.8, 1.0);

    public static Color Red => new Color(1.0, 0.0, 0.0);

    public static Color Blue => new Color(0.0, 0.0, 1.0);

    public static Color GreyRed => new Color(0.7, 0.3, 0.3);

    public static Color GreyRed2 => new Color(0.7, 0.6, 0.5);

    public static Color GreyBlue => new Color(0.1, 0.2, 0.5);

    public static Color Yellow => new Color(0.8, 0.8, 0.0);

    public static Color Orange => new Color(0.8, 0.6, 0.2);

    public static Color LightGrey => new Color(0.8, 0.8, 0.8);

    public static Color Grey => new Color(0.5, 0.5, 0.5);

    public static Color DarkRed => new Color(0.4, 0.2, 0.1);

    public static Color DarkOlive => new Color(0.2, 0.3, 0.1);
}

public static class ColorExtensions
{
    public static Color Sum(this IEnumerable<Color> colors) => new Color(colors.Sum(c => c.R), colors.Sum(c => c.G), colors.Sum(c => c.B));
}