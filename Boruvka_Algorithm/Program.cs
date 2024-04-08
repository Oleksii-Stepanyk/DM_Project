using Gen = Boruvka_Algorithm.Generator;

var rand = new Random();
var vertices = Gen.GenerateVertices(rand.Next(20, 201));
var graph = Gen.GenerateGraph(vertices, 0.9);
Console.WriteLine();