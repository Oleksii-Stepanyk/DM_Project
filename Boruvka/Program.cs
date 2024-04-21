using System.Diagnostics;
using Boruvka_Algorithm;
using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
var stopWatch = new Stopwatch();
var probabilities = new List<int> { 50, 75, 100 };
var vertices = new List<int> { 25, 60, 95, 130, 165 };
foreach (var minVertices in vertices)
{
    foreach (var probability in probabilities)
    {
        var experiment1 = GetAvgTimeList(probability, minVertices);
        var experiment2 = GetAvgTimeMatrix(probability, minVertices);
        Console.WriteLine(string.Join(", ", experiment1.Item1.ConvertAll(input => input.ToString()).ToArray()));
        Console.WriteLine(experiment1.Item2.ToString());
        Console.WriteLine();
        Console.WriteLine(string.Join(", ", experiment2.Item1.ConvertAll(input => input.ToString()).ToArray()));
        Console.WriteLine(experiment2.Item2.ToString(), "\r");
        Console.WriteLine();
    }
}

return;

(List<TimeSpan>, TimeSpan) GetAvgTimeMatrix(int probability, int minVertices)
{
    var time_spans = new List<TimeSpan>();
    for (int i = 0; i < 20; i++)
    {
        var vertices = Gen.GenerateVertices(rand.Next(minVertices, minVertices + 35));
        var graph = Gen.GenerateGraph(vertices, (double)probability / 100);
        var matrix = graph.AdjacencyMatrix();
        stopWatch.Start();
        var mst = new Boruvka().FindMST(matrix);
        stopWatch.Stop();
        time_spans.Add(stopWatch.Elapsed);
        stopWatch.Reset();
    }

    var average = new TimeSpan(Convert.ToInt64(time_spans.Average(t => t.Ticks)));
    return (time_spans, average);
}

(List<TimeSpan>, TimeSpan) GetAvgTimeList(int probability, int minVertices)
{
    var time_spans = new List<TimeSpan>();
    for (int i = 0; i < 20; i++)
    {
        var vertices = Gen.GenerateVertices(rand.Next(minVertices, minVertices + 35));
        var graph = Gen.GenerateGraph(vertices, (double)probability / 100);
        var list = graph.AdjacencyList();
        stopWatch.Start();
        var mst = new Boruvka().FindMST(list);
        stopWatch.Stop();
        time_spans.Add(stopWatch.Elapsed);
        stopWatch.Reset();
    }

    var average = new TimeSpan(Convert.ToInt64(time_spans.Average(t => t.Ticks)));
    return (time_spans, average);
}