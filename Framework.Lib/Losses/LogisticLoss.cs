namespace Framework.Lib.Losses;

public class LogisticLoss : LossFunction
{
    public override double Calculate(double[,] predicted, double[,] real)
    {
        var p = predicted.Cast<double>();
        var r = real.Cast<double>();
        return p.Zip(r).Sum(e =>
        {
            var part1 = e.First * Math.Log(e.Second);
            var part2 = (1 - e.First) * Math.Log(1 - e.Second);
            return -part1 - part2;
        });
    }
}
