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
            Weights[i] = new double[layerSizes[i + 1], layerSizes[i] + 1];
            for (int j = 0; j < layerSizes[i + 1]; j++)
            {
                for (int k = 0; k < layerSizes[i] + 1; k++)
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

    private double[][,] GetDerivativesNumericall(double[,] x, double[,] y, double epsilon = 1e-4)
    {
        var derivatives = new double[Weights.Length][,];
        for (int i = 0; i < Weights.Length; i++)
        {
            derivatives[i] = new double[Weights[i].GetLength(0), Weights[i].GetLength(1)];
            for (int j = 0; j < Weights[i].GetLength(0); j++)
            {
                for (int k = 0; k < Weights[i].GetLength(1); k++)
                {
                    Weights[i][j, k] += epsilon;
                    var lossPlus = Loss(x, y);
                    Weights[i][j, k] -= 2 * epsilon;
                    var lossMinus = Loss(x, y);
                    Weights[i][j, k] += epsilon;
                    derivatives[i][j, k] = (lossPlus - lossMinus) / (2 * epsilon);
                }
            }
        }

        return derivatives;
    }

    public void Fit(double[,] x, double[,] y, int epochCount = 5)
    {
        for (int i = 0; i < epochCount; i++)
        {
            var derivativaes = GetDerivativesNumericall(x, y);
            Optimizator.Optimize(Weights, derivativaes);
            var loss = Loss(x, y);
            Console.WriteLine($"Loss after epoch {i}: {loss}");
        }
    }
}
