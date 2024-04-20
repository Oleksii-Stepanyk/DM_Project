namespace Boruvka_Algorithm;

public static class Generator
{
    public static Graph GenerateGraph(List<Vertex> vertices, double probability) // Erdős–Rényi model
    {
        Graph graph = new Graph(vertices);
        Random rand = new Random();

        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                if ((double)rand.NextInt64(1, 100) / 100 < probability)
                {
                    Vertex vertex1 = vertices.ElementAt(i);
                    Vertex vertex2 = vertices.ElementAt(j);
                    Edge edge = new Edge(vertex1, vertex2, rand.Next(1, 75));
                    graph.AddEdge(edge);
                }
            }
        }

        return graph;
    }

    public static List<Vertex> GenerateVertices(int count)
    {
        var vertices = new List<Vertex>();
        for (int i = 0; i < count; i++)
        {
            vertices.Add(new Vertex(i.ToString()));
        }

        return vertices;
    }
}