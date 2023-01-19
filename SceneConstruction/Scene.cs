using Raytracer.Raytracing;
using Raytracer.Structure;

namespace Raytracer.SceneConstruction;

public class Scene
{
    public IStructure World;

    public CameraSettings CameraSettings;

    public Scene(IStructure world, CameraSettings cameraSettings)
    {
        World = world;
        CameraSettings = cameraSettings;
    }
}