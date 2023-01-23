using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Vector;

namespace Raytracer.SceneConstruction;

public class Scene
{
    public IStructure World;

    public CameraSettings CameraSettings;

    public Color Background;

    public Scene(IStructure world, CameraSettings cameraSettings, Color background)
    {
        World = world;
        CameraSettings = cameraSettings;
        Background = background;
    }
}