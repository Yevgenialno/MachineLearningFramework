namespace Framework.Lib.Activations;

public class Sigmoid : ActivationFunction
{
    public override double Forward(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }
}
