namespace Framework.Lib.Activations;

public class Relu : ActivationFunction
{
    public override double Forward(double x)
    {
        return Math.Max(x, 0);
    }
}
