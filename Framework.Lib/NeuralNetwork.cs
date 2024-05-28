using Framework.Lib.Activations;
using Framework.Lib.Losses;
using Framework.Lib.Optimizators;

namespace Framework.Lib;

public class NeuralNetwork
{
    public required IOptimizator Optimizator { get; set; }

    public required ActivationFunction Activation { get; set; }

    public required LossFunction LossFunction {  get; set; } 

    public double[][,] Weights { get; }

    public NeuralNetwork(int[] layerSizes)
    {
        Weights = new double[layerSizes.Length - 1][,];
        var r = new Random();
        for (int i = 0; i < layerSizes.Length - 1; i++)
        {
            Weights[i] = new double[layerSizes[i] + 1, layerSizes[i]];
            for (int j = 0; j < layerSizes[i] + 1; j++)
            {
                for (int k = 0; k < layerSizes[i]; k++)
                {
                    Weights[i][j, k] = (r.NextDouble() * 2) - 1;
                }
            }
        }
    }

    public double[,] Predict(double[,] x)
    {
        double[,] previousLayerOutput = x;
        for (int l = 0; l < Weights.Length - 1; l++)
        {
            double[,] layerOutput = Activation.Forward(Weights[l].Multipliy(previousLayerOutput));
            previousLayerOutput = layerOutput;
        }

        return Weights[^1].Multipliy(previousLayerOutput);
    }

    public double Loss(double[,] x, double[,] y)
    {
        return LossFunction.Calculate(Predict(x), y);
    }
}
