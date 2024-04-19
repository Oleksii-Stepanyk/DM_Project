namespace Boruvka_Algorithm;

public class Boruvka
{
    public Graph FindMST(Graph graph)
    {
        var mst = new Graph(graph.Vertices);
        var components = new List<Graph>();
        var cheapest = new Dictionary<Vertex, Edge>();
        var zeroVertex = new Vertex("0");
        var infEdge = new Edge(zeroVertex, zeroVertex, int.MaxValue);

        mst.Vertices.ForEach(v => components.Add(new Graph([v])));

        foreach (var vertex in mst.Vertices)
        {
            cheapest.Add(vertex, infEdge);
        }

        while (components.Count != 1)
        {
            foreach (var vertex in mst.Vertices)
            {
                cheapest[vertex] = infEdge;
            }

            foreach (var edge in graph.Edges)
            {
                if (InSameComponent(edge.Vertex1, edge.Vertex2, components))
                    continue;
                if (cheapest[edge.Vertex1] == infEdge || cheapest[edge.Vertex1].Weight > edge.Weight)
                    cheapest[edge.Vertex1] = edge;
                if (cheapest[edge.Vertex2] == infEdge || cheapest[edge.Vertex2].Weight > edge.Weight)
                    cheapest[edge.Vertex2] = edge;
            }

            foreach (var vertex in mst.Vertices)
            {
                Edge edge = cheapest[vertex];

                if (edge != infEdge)
                {
                    if (!InSameComponent(edge.Vertex1, edge.Vertex2, components))
                    {
                        mst.AddEdge(edge);
                        var graph1 = components.Find(c => c.Vertices.Contains(edge.Vertex1));
                        var graph2 = components.Find(c => c.Vertices.Contains(edge.Vertex2));
                        components.Remove(graph1);
                        components.Remove(graph2);
                        components.Add(new Graph(graph1, graph2));
                    }
                }
            }
        }

        return mst;
    }

    private bool InSameComponent(Vertex vertex1, Vertex vertex2, List<Graph> components)
    {
        return components.Any(component => component.Adjacent(vertex1, vertex2));
    }
}