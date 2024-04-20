using System.Diagnostics;
using Boruvka_Algorithm;
using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
var stopWatch = new Stopwatch();
for (int i = 0; i < 200; i++)
{
    var probability = rand.Next(55, 100);
    var vertices = Gen.GenerateVertices(rand.Next(20, 70));
    var graph = Gen.GenerateGraph(vertices, (double)probability / 100);
    var list = graph.AdjacencyList();
    stopWatch.Start();
    var mst1 = new Boruvka().FindMST(list);
    stopWatch.Stop();
    Console.WriteLine(stopWatch.Elapsed);
    stopWatch.Reset();
    var matrix = graph.AdjacencyMatrix();
    stopWatch.Start();
    var mst2 = new Boruvka().FindMST(matrix);
    stopWatch.Stop();
    Console.WriteLine(stopWatch.Elapsed);
    stopWatch.Reset();
    Console.WriteLine();
}