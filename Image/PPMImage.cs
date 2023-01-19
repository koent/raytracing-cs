using Raytracer.Vector;
using System.Text;

namespace Raytracer.Image;

public class PPMImage
{
    public int Width;

    public int Height;

    private Color[] Colors;

    public PPMImage(int width, int height)
    {
        Width = width;
        Height = height;
        Colors = new Color[width * height];
    }

    public Color this[int x, int y] => Colors[(Height - y - 1) * Width + x];

    public void Draw(int x, int y, Color color) => Colors[(Height - y - 1) * Width + x] = color;

    public void Draw(int x, int y, Color color, int nofSamples) => Draw(x, y, color / nofSamples);

    public override string ToString()
    {
        int maxLength = 3 + 10 + 4 + 12 * Colors.Length;
        StringBuilder sb = new StringBuilder("P3\n", maxLength);
        sb.AppendLine($"{Width} {Height}");
        sb.AppendLine("255");
        foreach (var color in Colors)
            sb.AppendLine(ToPPMLine(color));
        return sb.ToString();
    }

    private static string ToPPMLine(Color color)
    {
        int r = (int)(Math.Sqrt(color.R) * 255.999);
        int g = (int)(Math.Sqrt(color.G) * 255.999);
        int b = (int)(Math.Sqrt(color.B) * 255.999);
        return $"{r} {g} {b}";
    }

    public static PPMImage Combine(PPMImage[] images)
    {
        int nofSamples = images.Length;
        if (nofSamples == 0)
            throw new ArgumentException($"Images empty");

        var result = new PPMImage(images[0].Width, images[0].Height);
        for (int i = 0; i < result.Colors.Length; i++)
            result.Colors[i] = images.Select(image => image.Colors[i]).Sum() / nofSamples;

        return result;
    }
}