﻿using Raytracer.Image;
using Raytracer.Raytracing;
using Raytracer.SceneConstruction;
using System.Diagnostics;

// Image settings
const double aspectRatio = 16.0 / 9.0;
const int imageWidth = 800;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int nofSamples = 16;
const int maxDepth = 50;

// Set up renderer
var scene = TutorialScenes.TwoPerlinSpheres();
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
