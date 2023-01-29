using System.Text;
using Raytracer.Vector;

namespace Raytracer.Image;

public class PPM : IImageFormat
{
    public void Save(ImageData image)
    {
        var maxLength = 3 + 10 + 4 + 12 * image.Width * image.Height;
        var sb = new StringBuilder("P3\n", maxLength);
        sb.AppendLine($"{image.Width} {image.Height}");
        sb.AppendLine("255");
        for (var row = image.Height - 1; row >= 0; row--)
            for (var col = 0; col < image.Width; col++)
                sb.AppendLine(ToPPMLine(image[col, row]));

        Console.WriteLine(sb.ToString());
    }

    private static int Clamp(double value) => Clamp((int)value);

    private static int Clamp(int value) => value < 0 ? 0 : (value > 255 ? 255 : value);

    private static string ToPPMLine(Color color)
    {
        var r = Clamp(Math.Sqrt(color.R) * 255.999);
        var g = Clamp(Math.Sqrt(color.G) * 255.999);
        var b = Clamp(Math.Sqrt(color.B) * 255.999);
        return $"{r} {g} {b}";
    }
}