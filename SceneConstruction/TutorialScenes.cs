using Raytracer.Helpers;
using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Structure.BVH;
using Raytracer.Texture;
using Raytracer.Vector;

namespace Raytracer.SceneConstruction;

public static class TutorialScenes
{
    public static Scene ThreeSpheres()
    {
        // World
        var world = new StructureList();

        IMaterial materialGround = new Lambertian(Color.Yellow);
        IMaterial materialCenter = new NormalLambertian();
        IMaterial materialLeft = new Dielectric(1.5);
        IMaterial materialRight = new Metal(Color.Orange, 0.0);

        world.Add(new Sphere(new Point3(0.0, -100.5, -1.0), 100, materialGround));
        world.Add(new Sphere(new Point3(0.0, 0.0, -1.0), 0.5, materialCenter));
        world.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.49, materialLeft));
        world.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), -0.45, materialLeft));
        world.Add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, materialRight));

        // Camera
        var lookFrom = new Point3(-2.0, 2.0, 1.0);
        var lookAt = new Point3(0.0, 0.0, -1.0);
        var cameraSettings = new CameraSettings(lookFrom, lookAt, 40, 0.05, 0.0, 0.0);

        return new Scene(world, cameraSettings);
    }

    public static Scene BookCover()
    {
        bool moving = true;

        // World
        var random = RandomHelper.Instance;
        var world = new StructureList();

        var checker = new Checkerboard(Color.DarkOlive, Color.LightGrey);
        var materialGround = new Lambertian(checker);
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, materialGround));

        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                var selectMaterial = random.NextDouble();
                var center = new Point3(a + 0.9 * random.NextDouble(), 0.2, b + 0.9 * random.NextDouble());
                if (new Vec3(new Point3(4, 0.2, 0), center).LengthSquared > 0.8)
                {
                    IMaterial materialSphere;
                    if (selectMaterial < 0.8)
                    {
                        // Diffuse
                        var albedo = Color.Random() * Color.Random();
                        materialSphere = new Lambertian(albedo);
                        var centerTo = center + new Vec3(0, random.NextDouble(0, 0.5), 0);
                        world.Add(new MovingSphere(center, moving ? centerTo : center, 0.0, 1.0, 0.2, materialSphere));
                    }
                    else if (selectMaterial < 0.95)
                    {
                        // Metal
                        var albedo = Color.Random(0.5, 1);
                        var fuzz = random.NextDouble(0, 0.5);
                        materialSphere = new Metal(albedo, fuzz);
                        world.Add(new Sphere(center, 0.2, materialSphere));
                    }
                    else
                    {
                        // Glass
                        materialSphere = new Dielectric(1.5);
                        world.Add(new Sphere(center, 0.2, materialSphere));
                    }
                }
            }
        }

        var material1 = new Dielectric(1.5);
        world.Add(new Sphere(new Point3(0, 1, 0), 1.0, material1));

        var material2 = new Lambertian(Color.DarkRed);
        world.Add(new Sphere(new Point3(-4, 1, 0), 1.0, material2));

        var material3 = new Metal(Color.GreyRed2, 0.0);
        world.Add(new Sphere(new Point3(4, 1, 0), 1.0, material3));

        // Camera
        var lookFrom = new Point3(13, 2, 3);
        var lookAt = new Point3(0, 0, 0);
        var fieldOfView = 20.0;
        var aperture = 0.1;
        var focusDistance = 10.0;
        var timeFrom = 0.0;
        var timeTo = 1.0;
        var cameraSettings = new CameraSettings(lookFrom, lookAt, fieldOfView, aperture, focusDistance, timeFrom, timeTo);

        return new Scene(new BVHNode(world, timeFrom, timeTo), cameraSettings);
    }

    public static Scene CheckeredSpheres()
    {
        // World
        var world = new StructureList();
        var checker = new Checkerboard(Color.DarkOlive, Color.LightGrey);
        world.Add(new Sphere(new Point3(0, -10, 0), 10, new Lambertian(checker)));
        world.Add(new Sphere(new Point3(0, 10, 0), 10, new Lambertian(checker)));

        // Camera
        var lookFrom = new Point3(13, 2, 3);
        var lookAt = new Point3(0, 0, 0);
        var fieldOfView = 20.0;
        var aperture = 0.0;
        var focusDistance = 10.0;

        var cameraSettings = new CameraSettings(lookFrom, lookAt, fieldOfView, aperture, focusDistance, 0.0, 0.0);
        return new Scene(world, cameraSettings);
    }
}