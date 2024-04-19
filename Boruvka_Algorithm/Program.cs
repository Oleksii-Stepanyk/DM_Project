using Boruvka_Algorithm;
using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
for (int i = 0; i < 200; i++)
{
    var vertices = Gen.GenerateVertices(rand.Next(5, 10));
    var graph = Gen.GenerateGraph(vertices, rand.Next(50, 100) / 100);
    var mst = new Boruvka().FindMST(graph);
    Console.WriteLine();
}