using Raytracer.Image;
using Raytracer.Raytracing;
using Raytracer.SceneConstruction;
using System.Diagnostics;

// Image settings
const double aspectRatio = 3.0 / 2.0;
const int imageWidth = 600;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int nofSamples = 100;
const int maxDepth = 50;

// Set up renderer
var scene = TutorialScenes.BookCover();
var camera = new Camera(scene.CameraSettings, aspectRatio);
var renderer = new Renderer(scene.World, camera, maxDepth, imageWidth, imageHeight);

// Render
var sw = new Stopwatch();
sw.Start();
PPMImage image = renderer.ParallelRender(nofSamples);
sw.Stop();
Console.Error.WriteLine();
Console.Error.WriteLine($"Elapsed time: {sw.Elapsed}");
Console.WriteLine(image);
