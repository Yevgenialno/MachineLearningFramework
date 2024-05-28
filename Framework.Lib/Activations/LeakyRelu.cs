namespace Framework.Lib.Activations;

public class LeakyRelu : ActivationFunction
{
    public double A { get; set; } = 0.01;

    public override double Forward(double x)
    {
        return Math.Max(A * x, x);
    }
}
