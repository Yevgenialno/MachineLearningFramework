namespace Framework.Lib.Activations;

public class TanH : ActivationFunction
{
    public override double Forward(double x)
    {
        return Math.Tanh(x);
    }
}
