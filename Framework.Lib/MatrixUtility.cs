namespace Framework.Lib;

public static class MatrixUtility
{
    public static double[,] Multipliy(this double[,] a, double[,] b)
    {
        if (a.GetLength(1) != b.GetLength(0))
        {
            throw new InvalidOperationException($"Imposible to multiply matrices. Sizes are ({a.GetLength(0)}, {a.GetLength(1)}) and ({b.GetLength(0)}, {b.GetLength(1)})");
        }

        var result = new double[a.GetLength(0), b.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                double elem = 0;
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    elem += a[i, k] * b[k, j];
                }

                result[i, j] = elem;
            }
        }

        return result;
    }

    public static double[,] Multiply(this double[,] a, double b)
    {
        var c = new double[1, 1];
        c[0, 0] = b;
        return Multipliy(a, c);
    }

    public static double[,] Add(this double[,] a, double[,] b)
    {
        if ((a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
        {
            throw new InvalidOperationException($"Imposible to add matrices. Sizes are ({a.GetLength(0)}, {a.GetLength(1)}) and ({b.GetLength(0)}, {b.GetLength(1)})");
        }

        var result = new double[a.GetLength(0), b.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }

        return result;
    }

    public static double[,] Subtract(this double[,] a, double[,] b)
    {
        return Add(a, Multiply(b, -1));
    }
}
