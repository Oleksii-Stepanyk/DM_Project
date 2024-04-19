using Boruvka_Algorithm;
using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
for (int i = 0; i < 200; i++)
{
    var probability = rand.Next(50, 100);
    var vertices = Gen.GenerateVertices(rand.Next(10, 100));
    var graph = Gen.GenerateGraph(vertices, ((double)probability / 100));
    var mst = new Boruvka().FindMST(graph);
    Console.WriteLine();
}