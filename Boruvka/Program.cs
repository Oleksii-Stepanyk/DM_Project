using System.Diagnostics;
using Boruvka_Algorithm;
using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
var stopWatch = new Stopwatch();
var probabilities = new List<double> {0.52, 0.64, 0.76, 0.88, 1.0 };
var vertices = new List<int> { 20, 38, 56, 74, 92, 110, 128, 146, 164, 182 };
foreach (var minVertices in vertices)
{
    foreach (var probability in probabilities)
    {
        var experiment1 = GetAvgTimeList(probability, minVertices);
        var experiment2 = GetAvgTimeMatrix(probability, minVertices);
        Console.WriteLine(
            $"Method: List, Prob:{probability * 100}%, Vertices Range: {minVertices} - {minVertices + 18}, " +
            $"{string.Join(", ", experiment1.Item1.ConvertAll(input => input.ToString()).ToArray())}");
        Console.WriteLine($"Average time: {experiment1.Item2.ToString()}");
        Console.WriteLine();
        Console.WriteLine(
            $"Method: Matrix, Prob:{probability * 100}%, Vertices Range: {minVertices} - {minVertices + 18}, " +
            $"{string.Join(", ", experiment2.Item1.ConvertAll(input => input.ToString()).ToArray())}");
        Console.WriteLine($"Average time: {experiment2.Item2.ToString()}");
        Console.WriteLine();
    }

    Console.ReadLine();
}

return;

(List<TimeSpan>, TimeSpan) GetAvgTimeMatrix(double probability, int minVertices)
{
    var time_spans = new List<TimeSpan>();
    for (int i = 0; i < 20; i++)
    {
        var vertices = Gen.GenerateVertices(rand.Next(minVertices, minVertices + 18));
        var graph = Gen.GenerateGraph(vertices, probability);
        var matrix = graph.AdjacencyMatrix();
        stopWatch.Start();
        var mst = new Boruvka().FindMST(matrix);
        stopWatch.Stop();
        time_spans.Add(stopWatch.Elapsed);
        stopWatch.Reset();
        vertices = null;
        graph = null;
        matrix = null;
        mst = null;
    }

    var average = new TimeSpan(Convert.ToInt64(time_spans.Average(t => t.Ticks)));
    return (time_spans, average);
}

(List<TimeSpan>, TimeSpan) GetAvgTimeList(double probability, int minVertices)
{
    var time_spans = new List<TimeSpan>();
    for (int i = 0; i < 20; i++)
    {
        var vertices = Gen.GenerateVertices(rand.Next(minVertices, minVertices + 18));
        var graph = Gen.GenerateGraph(vertices, probability);
        var list = graph.AdjacencyList();
        stopWatch.Start();
        var mst = new Boruvka().FindMST(list);
        stopWatch.Stop();
        time_spans.Add(stopWatch.Elapsed);
        stopWatch.Reset();
        vertices = null;
        graph = null;
        list = null;
        mst = null;
    }
    var average = new TimeSpan(Convert.ToInt64(time_spans.Average(t => t.Ticks)));
    return (time_spans, average);
}