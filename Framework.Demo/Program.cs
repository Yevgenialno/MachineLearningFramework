using Framework.Lib;
using Framework.Lib.Activations;
using Framework.Lib.Losses;
using Framework.Lib.Optimizators;

var model = new NeuralNetwork([1, 1])
{
    Activation = new Relu(),
    LossFunction = new MeanSquaredErrorLoss(),
    Optimizator = new GradientDescentOptimizator() { Alpha = 0.01 },
};

double[,] x = {
    { 1, 2, 3, 4 },
};
double[,] y =
{
    { 1, 2, 3, 4 },
};
var p = model.Predict(x);
Console.WriteLine($"{p[0, 0]} {p[0, 1]} {p[0, 2]} {p[0, 3]}");
model.Fit(x, y, 1000);
p = model.Predict(y);
Console.WriteLine($"{p[0, 0]} {p[0, 1]} {p[0, 2]} {p[0, 3]}");