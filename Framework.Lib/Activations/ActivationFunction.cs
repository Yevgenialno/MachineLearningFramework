namespace Framework.Lib.Activations;

public abstract class ActivationFunction
{
    public abstract double Forward(double x);

    public double[,] Forward(double[,] x)
    {
        double[,] result = new double[x.GetLength(0), x.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = Forward(x[i, j]);
            }
        }

        return result;
    }

}
