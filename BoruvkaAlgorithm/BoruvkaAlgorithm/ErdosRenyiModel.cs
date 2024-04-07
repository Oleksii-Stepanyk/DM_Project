namespace BoruvkaAlgorithm;

class ErdosRenyiModel(HashSet<Vertex> vertices, double probability)
{
    public Graph GenerateGraph()
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
}