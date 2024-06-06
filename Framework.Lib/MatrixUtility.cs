namespace Framework.Lib;

public static class MatrixUtility
{
    public static double[,] Multiply(this double[,] a, double[,] b, bool addOnes = true)
    {
        if (a.GetLength(1) != b.GetLength(0) + Convert.ToInt32(addOnes))
        {
            throw new InvalidOperationException($"Imposible to multiply matrices. Sizes are ({a.GetLength(0)}, {a.GetLength(1)}) and ({b.GetLength(0)}, {b.GetLength(1)})");
        }

        var result = new double[a.GetLength(0), b.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                double elem = addOnes ? a[i, 0] : 0;
                for (int k = 0; k < b.GetLength(0); k++)
                {
                    elem += a[i, k + Convert.ToInt32(addOnes)] * b[k, j];
                }

                result[i, j] = elem;
            }
        }

        return result;
    }

    public static double[,] Multiply(this double[,] a, double b)
    {
        var c = new double[a.GetLength(0), a.GetLength(1)];
        for (int i = 0; i < c.GetLength(0); i++)
        {
            for (int j = 0;  j < c.GetLength(1); j++)
            {
                c[i, j] = b * a[i, j];
            }
        }

        return c;
    }

    public static double[,] ElementWise(this double[,] a, Func<double, double> f)
    {
        var result = new double[a.GetLength(0), a.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = f(a[i, j]);
            }
        }

        return result;
    }

    public static double[,] ElementWise(this double[,] a, double[,] b, Func<double, double, double> f)
    {
        var result = new double[a.GetLength(0), a.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = f(a[i, j], b[i, j]);
            }
        }

        return result;
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
