using BoruvkaAlgorithm;

static HashSet<Vertex> GenerateVertices(int count)
{
    var vertices = new HashSet<Vertex>();
    for (int i = 0; i < count; i++)
    {
        vertices.Add(new Vertex(i.ToString()));
    }
    return vertices;
}

var rand = new Random();
var vertices = GenerateVertices(rand.Next(20, 201));
var model = new ErdosRenyiModel(vertices, 0.75);
var graph = model.GenerateGraph();
Console.WriteLine();