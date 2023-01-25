using Raytracer.Helpers;
using Raytracer.Vector;

public class Perlin
{
    private static int NofPoints = 256;

    private Vec3[] RandomVector;

    private int[] PermutationX, PermutationY, PermutationZ;

    public Perlin()
    {
        RandomVector = new Vec3[NofPoints];
        for (var i = 0; i < NofPoints; i++)
            RandomVector[i] = Vec3.Random(-1, 1).UnitVector;


        PermutationX = GeneratePerlinPermutation();
        PermutationY = GeneratePerlinPermutation();
        PermutationZ = GeneratePerlinPermutation();
    }

    public double Noise(Point3 point)
    {
        var i = Math.Floor(point.X);
        var j = Math.Floor(point.Y);
        var k = Math.Floor(point.Z);
        var c = new Vec3[2, 2, 2];

        for (var di = 0; di < 2; di++)
            for (var dj = 0; dj < 2; dj++)
                for (var dk = 0; dk < 2; dk++)
                    c[di, dj, dk] = RandomVector[PermutationX[(int)(i + di) & 255] ^ PermutationY[(int)(j + dj) & 255] ^ PermutationZ[(int)(k + dk) & 255]];

        return Interpolate(c, point.X - i, point.Y - j, point.Z - k);
    }

    public double Turbulence(Point3 point, int depth = 7)
    {
        var result = 0.0;
        var weight = 1.0;

        for (var i = 0; i < depth; i++)
        {
            result += weight * Noise(point);
            weight /= 2;
            point = 2 * point;
        }

        return Math.Abs(result);
    }

    private static double Hermite(double value) => value * value * (3 - 2 * value);

    private int[] GeneratePerlinPermutation()
    {
        var permutation = new int[NofPoints];
        for (var i = 0; i < NofPoints; i++)
            permutation[i] = i;

        Permute(ref permutation);
        return permutation;
    }

    private static void Permute(ref int[] array)
    {
        for (var i = NofPoints - 1; i > 0; i--)
        {
            var target = RandomHelper.Instance.Next(i);
            (array[i], array[target]) = (array[target], array[i]);
        }
    }

    private static double Interpolate(Vec3[,,] c, double u, double v, double w)
    {
        var uu = Hermite(u);
        var vv = Hermite(v);
        var ww = Hermite(w);

        var result = 0.0;
        for (var di = 0; di < 2; di++)
            for (var dj = 0; dj < 2; dj++)
                for (var dk = 0; dk < 2; dk++)
                {
                    var weight = new Vec3(u - di, v - dj, w - dk);
                    result += Lerp(1 - u, u, di) * Lerp(1 - v, v, dj) * Lerp(1 - w, w, dk) * Vec3.Dot(c[di, dj, dk], weight);
                }

        return result;
    }

    private static double Lerp(double start, double end, double position) => (1 - position) * start + position * end;
}