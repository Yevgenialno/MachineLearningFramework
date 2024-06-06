using Framework.Lib;

namespace Framework.Lib.Optimizators;

public class AdamOptimizer : IOptimizator
{
    public double LearningRate { get; set; } = 1e-4;

    public double Beta1 { get; init; } = 0.9;

    public double Beta2 { get; init; } = 0.999;

    public double Epsilon { get; init; } = double.Epsilon;

    private double[][,]? m;

    private double[][,]? v;

    private double[][,]? mBias;

    private double[][,]? vBias;

    public void Optimize(double[][,] weights, double[][,] grads)
    {
        if (m is null || v is null || mBias is null || vBias is null)
        {
            m = new double[weights.Length][,];
            mBias = new double[weights.Length][,];
            v = new double[weights.Length][,];
            vBias = new double[weights.Length][,];
            for (int i = 0; i < weights.Length; i++)
            {
                m[i] = new double[weights[i].GetLength(0), weights[i].GetLength(1)];
                mBias[i] = new double[weights[i].GetLength(0), weights[i].GetLength(1)];
                v[i] = new double[weights[i].GetLength(0), weights[i].GetLength(1)];
                vBias[i] = new double[weights[i].GetLength(0), weights[i].GetLength(1)];
            }

        }

        for (int i = 0; i < weights.Length; i++)
        {
            m[i] = m[i].Multiply(Beta1).Add(grads[i].Multiply(1 - Beta1));
            v[i] = v[i].Multiply(Beta2).Add(grads[i].ElementWise(grads[i], (a, b) => a * b).Multiply(1 - Beta2));
            mBias[i] = m[i].Multiply(1 / (1 - Beta1));
            vBias[i] = v[i].Multiply(1 / (1 - Beta2));
            LearningRate *= Math.Sqrt(1 - Beta2) / (1 - Beta1);
            weights[i] = weights[i].Subtract(mBias[i].ElementWise(vBias[i].ElementWise(a => Math.Sqrt(a) + Epsilon), (a, b) => a / b).Multiply(LearningRate));
        }
    }
}
