using System.Text;
using Raytracer.Vector;

namespace Raytracer.Image;

public class PPM : IImageFormat
{
    public void Save(ImageData image, string filename)
    {
        using (StreamWriter sw = File.CreateText(filename + ".ppm"))
        {
            var maxLength = 3 + 10 + 4 + 12 * image.Width * image.Height;
            sw.WriteLine("P3");
            sw.WriteLine($"{image.Width} {image.Height}");
            sw.WriteLine("255");
            for (var row = image.Height - 1; row >= 0; row--)
                for (var col = 0; col < image.Width; col++)
                    sw.WriteLine(ToPPMLine(image[col, row]));
        }
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