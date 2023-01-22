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
        int x = (int)(4 * point.X) & 255;
        int y = (int)(4 * point.Y) & 255;
        int z = (int)(4 * point.Z) & 255;

        return randomDouble[PermutationX[x] ^ PermutationY[y] ^ PermutationZ[z]];
    }

    private int[] GeneratePerlinPermutation()
    {
        var permutation = new int[nofPoints];
        for (int i = 0; i < nofPoints; i++)
            permutation[i] = i;

        Permute(ref permutation);
        return permutation;
    }

    static void Permute(ref int[] array)
    {
        for (int i = nofPoints - 1; i > 0; i--)
        {
            int target = RandomHelper.Instance.Next(i);
            (array[i], array[target]) = (array[target], array[i]);
        }
    }
}