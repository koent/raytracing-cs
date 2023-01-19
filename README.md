# Raytracing in C#
We follow the book series 'Raytracing In One Weekend', but in C#.

## Additions wrt. the books
- A `Renderer` class with different renderers.
  - Including a parallel (per sample) renderer.
- Separate classes for `Color`, `Point3` and `Vec3`.
- Storing scenes (world and camera settings) in a `Scene` class.
  - Static constructors for different scenes.
- A separate class for storing the image.

## Usage
```bash
dotnet run > image.ppm
feh image.ppm
```

## Links
- https://raytracing.github.io/books/RayTracingInOneWeekend.html
- https://raytracing.github.io/books/RayTracingTheNextWeek.html
- https://raytracing.github.io/books/RayTracingTheRestOfYourLife.html
- https://iquilezles.org/articles/intersectors/