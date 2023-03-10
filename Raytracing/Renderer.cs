using Raytracer.Helpers;
using Raytracer.Image;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.Raytracing;

public class Renderer
{
    public IStructure World;

    public Camera Camera;

    public Color Background;

    public int MaxDepth;

    public int ImageWidth, ImageHeight;

    public Renderer(IStructure world, Camera camera, Color background, int maxDepth, int imageWidth, int imageHeight)
    {
        World = world;
        Camera = camera;
        Background = background;
        MaxDepth = maxDepth;
        ImageWidth = imageWidth;
        ImageHeight = imageHeight;
    }

    public ImageData LineRender(int nofSamples)
    {
        var image = new ImageData(ImageWidth, ImageHeight);
        for (var y = 0; y < image.Height; y++)
        {
            Console.Error.Write($"\rLines remaining: {image.Height - y - 1,3}");

            for (var x = 0; x < image.Width; x++)
            {
                var pixelColor = new Color();
                for (var sample = 0; sample < nofSamples; sample++)
                {
                    var u = (x + RandomHelper.Instance.NextDouble()) / (image.Width - 1);
                    var v = (y + RandomHelper.Instance.NextDouble()) / (image.Height - 1);
                    var ray = Camera.GetRay(u, v);
                    pixelColor.Add(ImageFunctions.RayColor(ray, World, Background, MaxDepth));
                }

                image.Draw(x, y, pixelColor, nofSamples);
            }
        }

        return image;
    }

    public ImageData SampleRender(int nofSamples) => ImageData.Combine(Enumerable.Range(0, nofSamples).Select(s => SingleRender(s, nofSamples)).ToArray());

    public ImageData ParallelRender(int nofSamples) => ImageData.Combine(Enumerable.Range(0, nofSamples).AsParallel().Select(s => SingleRender(s, nofSamples)).ToArray());

    private ImageData SingleRender(int sample, int nofSamples)
    {
        Console.Error.Write($"\rSample {sample + 1,3} of {nofSamples}");
        var image = new ImageData(ImageWidth, ImageHeight);
        for (var y = 0; y < image.Height; y++)
            for (var x = 0; x < image.Width; x++)
                RenderPixel(image, x, y);

        return image;
    }

    private void RenderPixel(ImageData image, int x, int y)
    {
        var u = (x + RandomHelper.Instance.NextDouble()) / (image.Width - 1);
        var v = (y + RandomHelper.Instance.NextDouble()) / (image.Height - 1);
        var ray = Camera.GetRay(u, v);
        var color = ImageFunctions.RayColor(ray, World, Background, MaxDepth);
        image.Draw(x, y, color);
    }
}