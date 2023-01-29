namespace Raytracer.Image;

public interface IImageFormat
{
    public void Save(ImageData image, string filename);
}