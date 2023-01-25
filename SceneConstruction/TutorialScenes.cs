using Raytracer.Helpers;
using Raytracer.Material;
using Raytracer.Raytracing;
using Raytracer.Structure;
using Raytracer.Structure.BVH;
using Raytracer.Structure.Transformation;
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

        return new Scene(world, cameraSettings, Color.LightSky);
    }

    public static Scene BookCover()
    {
        var moving = true;

        // World
        var random = RandomHelper.Instance;
        var world = new StructureList();

        var checker = new Checkerboard(Color.DarkOlive, Color.LightGrey);
        var materialGround = new Lambertian(checker);
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, materialGround));

        for (var a = -11; a < 11; a++)
        {
            for (var b = -11; b < 11; b++)
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

        return new Scene(new BVHNode(world, timeFrom, timeTo), cameraSettings, Color.LightSky);
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
        return new Scene(world, cameraSettings, Color.LightSky);
    }

    public static Scene TwoPerlinSpheres()
    {
        // World
        var world = new StructureList();
        var perlin = new Marble(scale: 4);
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, new Lambertian(perlin)));
        world.Add(new Sphere(new Point3(0, 2, 0), 2, new Lambertian(perlin)));

        // Camera
        var lookFrom = new Point3(13, 2, 3);
        var lookAt = new Point3(0, 0, 0);
        var fieldOfView = 20.0;
        var aperture = 0.0;
        var focusDistance = 10.0;

        var cameraSettings = new CameraSettings(lookFrom, lookAt, fieldOfView, aperture, focusDistance, 0.0, 0.0);
        return new Scene(world, cameraSettings, Color.LightSky);
    }

    public static Scene Light()
    {
        // World
        var world = new StructureList();

        var ground = new Marble(scale: 4);
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, new Lambertian(ground)));
        world.Add(new Sphere(new Point3(0, 2, 0), 2, new Lambertian(ground)));

        var light = new DiffuseLight(4);
        world.Add(new XYRectangle(3, 5, 1, 3, -2, light));

        // Camera
        var lookFrom = new Point3(26, 3, 6);
        var lookAt = new Point3(0, 2, 0);
        var fieldOfView = 20.0;
        var aperture = 0.0;
        var focusDistance = 10.0;

        var cameraSettings = new CameraSettings(lookFrom, lookAt, fieldOfView, aperture, focusDistance, 0.0, 0.0);
        return new Scene(world, cameraSettings, Color.Black);
    }

    public static Scene CornellBox()
    {
        // World
        var world = new StructureList();

        var red = new Lambertian(Color.CornellBoxRed);
        var white = new Lambertian(Color.CornellBoxWhite);
        var green = new Lambertian(Color.CornellBoxGreen);
        var light = new DiffuseLight(15);

        world.Add(new YZRectangle(0, 555, 0, 555, 555, green));
        world.Add(new YZRectangle(0, 555, 0, 555, 0, red));
        world.Add(new XZRectangle(213, 343, 227, 332, 554, light));
        world.Add(new XZRectangle(0, 555, 0, 555, 0, white));
        world.Add(new XZRectangle(0, 555, 0, 555, 555, white));
        world.Add(new XYRectangle(0, 555, 0, 555, 555, white));

        // world.Add(new Box(new Point3(130, 0, 65), new Point3(295, 165, 230), white));
        // world.Add(new Box(new Point3(265, 0, 295), new Point3(430, 330, 460), white));
        world.Add(new Box(new Point3(0, 0, 0), new Point3(165, 330, 165), white).RotateY(15).Translate(new Vec3(265, 0, 295)));
        world.Add(new Box(new Point3(0, 0, 0), new Point3(165, 165, 165), white).RotateY(-18).Translate(new Vec3(130, 0, 65)));

        // Camera
        var lookFrom = new Point3(278, 278, -800);
        var lookAt = new Point3(278, 278, 0);
        var fieldOfView = 40.0;
        var aperture = 0.0;
        var focusDistance = 10.0;

        var cameraSettings = new CameraSettings(lookFrom, lookAt, fieldOfView, aperture, focusDistance, 0.0, 0.0);
        return new Scene(world, cameraSettings, Color.Black);
    }
}