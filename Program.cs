using System.Diagnostics;
using Raytracer.Raytracing;
using Raytracer.SceneConstruction;

// Image settings
const double aspectRatio = 1.0;
const int imageWidth = 600;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int nofSamples = 16;
const int maxDepth = 50;

// Set up renderer
var scene = TutorialScenes.CornellBox();
var camera = new Camera(scene.CameraSettings, aspectRatio);
var renderer = new Renderer(scene.World, camera, scene.Background, maxDepth, imageWidth, imageHeight);

// Render
var sw = new Stopwatch();
sw.Start();
var image = renderer.ParallelRender(nofSamples);
sw.Stop();
Console.Error.WriteLine();
Console.Error.WriteLine($"Elapsed time: {sw.Elapsed}");
Console.WriteLine(image);
