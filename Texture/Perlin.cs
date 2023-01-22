using Raytracer.Helpers;
using Raytracer.Vector;

public class Perlin
{
    private static int nofPoints = 256;

    private double[] randomDouble;

    private int[] PermutationX, PermutationY, PermutationZ;

    public Perlin()
    {
        randomDouble = new double[nofPoints];
        for (int i = 0; i < nofPoints; i++)
            randomDouble[i] = RandomHelper.Instance.NextDouble();

        PermutationX = GeneratePerlinPermutation();
        PermutationY = GeneratePerlinPermutation();
        PermutationZ = GeneratePerlinPermutation();
    }

    public double Noise(Point3 point)
    {
        var i = Math.Floor(point.X);
        var j = Math.Floor(point.Y);
        var k = Math.Floor(point.Z);
        double[,,] c = new double[2, 2, 2];

        for (int di = 0; di < 2; di++)
            for (int dj = 0; dj < 2; dj++)
                for (int dk = 0; dk < 2; dk++)
                    c[di, dj, dk] = randomDouble[PermutationX[(int)(i + di) & 255] ^ PermutationY[(int)(j + dj) & 255] ^ PermutationZ[(int)(k + dk) & 255]];

        return Interpolate(c, point.X - i, point.Y - j, point.Z - k);
    }

    private int[] GeneratePerlinPermutation()
    {
        var permutation = new int[nofPoints];
        for (int i = 0; i < nofPoints; i++)
            permutation[i] = i;

        Permute(ref permutation);
        return permutation;
    }

    private static void Permute(ref int[] array)
    {
        for (int i = nofPoints - 1; i > 0; i--)
        {
            int target = RandomHelper.Instance.Next(i);
            (array[i], array[target]) = (array[target], array[i]);
        }
    }

    private static double Interpolate(double[,,] c, double u, double v, double w)
    {
        var result = 0.0;
        for (int di = 0; di < 2; di++)
            for (int dj = 0; dj < 2; dj++)
                for (int dk = 0; dk < 2; dk++)
                    result += Lerp(1 - u, u, di) * Lerp(1 - v, v, dj) * Lerp(1 - w, w, dk) * c[di, dj, dk];

        return result;
    }

    private static double Lerp(double start, double end, double position) => (1 - position) * start + position * end;
}