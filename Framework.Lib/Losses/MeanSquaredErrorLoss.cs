namespace Framework.Lib.Losses;

public class MeanSquaredErrorLoss : LossFunction
{
    public override double Calculate(double[,] predicted, double[,] real)
    {
        var res = predicted.Subtract(real).Cast<double>().ToArray();
        return res.Sum(el => el * el) / res.Length;
    }
}
