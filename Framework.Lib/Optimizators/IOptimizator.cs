namespace Framework.Lib.Optimizators;

public interface IOptimizator
{
    void Optimize(double[][,] weights, double[][,] grads);
}
