namespace Framework.Lib.Losses;

public abstract class LossFunction
{
    public abstract double Calculate(double[,] predicted, double[,] real);
}
