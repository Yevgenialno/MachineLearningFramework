namespace Framework.Lib.Optimizators;

internal class GradientDescentOptimizator : IOptimizator
{
    public double Alpha { get; set; } = 1e-4;

    public void Optimize(double[][,] weights, double[][,] grads)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i].Subtract(grads[i].Multiply(Alpha));
        }
    }
}
