using Framework.Lib;
using Framework.Lib.Activations;
using Framework.Lib.Losses;
using Framework.Lib.Optimizators;
using Framework.Lib.DataReaders;
using System.Runtime.InteropServices;

Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

//TASK 1
//double[,] x = {
//    { 1, 2, 3, 4 },
//};
//double[,] y =
//{
//    { 2, 4, 6, 8 },
//};

//var model = new NeuralNetwork([1, 1])
//{
//    Activation = new Relu(),
//    LossFunction = new MeanSquaredErrorLoss(),
//    Optimizator = new GradientDescentOptimizator() { LearningRate = 0.05 },
//};
//model.Fit(x, y, 100);
//var p = model.Predict(x);
//Console.WriteLine($"predicted: {p[0, 0]} {p[0, 1]} {p[0, 2]} {p[0, 3]}");
//Console.WriteLine($"real: {y[0, 1]} {y[0, 2]} {y[0, 3]} {y[0, 3]}");

//TASK 2
//var dataReader = new CsvFileDataReader("data1.csv", [1]);
//(double[,] x, double[,] y) = dataReader.Read();

//var model = new NeuralNetwork([1, 1])
//{
//    Activation = new Relu(),
//    LossFunction = new MeanSquaredErrorLoss(),
//    Optimizator = new GradientDescentOptimizator() { LearningRate = 0.025 },
//};
//model.Fit(x, y, 100);
//var p = model.Predict(x);
//Console.WriteLine($"model weights: a = {model.Weights[0][0, 1]}, b = {model.Weights[0][0, 0]}");
//Console.WriteLine($"predicted: {p[0, 0]} {p[0, 1]} {p[0, 2]} {p[0, 3]}");
//Console.WriteLine($"real: {y[0, 1]} {y[0, 1]} {y[0, 2]} {y[0, 3]}");

//TASK 3
double Denormalize(double x)
{
    double mean = 2844.946808510638;
    double std = 2080.7524867505836;
    return (x * std) + mean;
}

var dataReader = new CsvFileDataReader("real_data_last.csv", [7]);
(double[,] x, double[,] y) = dataReader.Read();

ActivationFunction[] activationFunctions = [new Relu(), new LeakyRelu(), new Sigmoid(), new TanH()];
int[][] architechtures = [[7, 1], [7, 16, 1], [7, 16, 16, 1]];

for (int i =  0; i < architechtures.Length; i++)
{
    foreach (ActivationFunction activationFunction in activationFunctions)
    {
        Console.WriteLine($"Running architecture {i} with activation {activationFunction.GetType().Name}");
        var model = new NeuralNetwork(architechtures[i])
        {
            Activation = activationFunction,
            LossFunction = new MeanSquaredErrorLoss(),
            Optimizator = new GradientDescentOptimizator() { LearningRate = 0.05 },
        };
        model.Fit(x, y, 1000);
        var p = model.Predict(x).ElementWise(Denormalize);
        Console.WriteLine($"predicted: {p[0, 0]} {p[0, 15]} {p[0, 25]} {p[0, 70]}");
        Console.WriteLine($"real: {Denormalize(y[0, 0])} {Denormalize(y[0, 15])} {Denormalize(y[0, 25])} {Denormalize(y[0, 70])}");
    }
}