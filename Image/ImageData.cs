using Raytracer.Vector;

namespace Raytracer.Image;

public class ImageData
{
    public int Width;

    public int Height;

    private Color[] Colors;

    public ImageData(int width, int height)
    {
        Width = width;
        Height = height;
        Colors = new Color[width * height];
    }

    public Color this[int x, int y] => Colors[(Height - y - 1) * Width + x];

    public void Draw(int x, int y, Color color) => Colors[(Height - y - 1) * Width + x] = color;

    public void Draw(int x, int y, Color color, int nofSamples) => Draw(x, y, color / nofSamples);

    public static ImageData Combine(ImageData[] images)
    {
        var nofSamples = images.Length;
        if (nofSamples == 0)
            throw new ArgumentException($"Images empty");

        var result = new ImageData(images[0].Width, images[0].Height);
        for (var i = 0; i < result.Colors.Length; i++)
            result.Colors[i] = images.Select(image => image.Colors[i]).Sum() / nofSamples;

        return result;
    }

    public void Save<ImageFormat>() where ImageFormat : IImageFormat, new() => new ImageFormat().Save(this);
}