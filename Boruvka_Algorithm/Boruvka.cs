namespace Boruvka_Algorithm;

public class Boruvka
{
    public Graph FindMST(Dictionary<Vertex, List<KeyValuePair<Vertex, int>>> adjacencyList)
    {
        var mst = new Graph(adjacencyList.Keys.ToList());
        var components = new List<Graph>();
        var cheapest = new Dictionary<Vertex, Edge>();
        var zeroVertex = new Vertex("0");
        var infEdge = new Edge(zeroVertex, zeroVertex, int.MaxValue);

        mst.Vertices.ForEach(v => components.Add(new Graph([v])));

        foreach (var vertex in mst.Vertices)
        {
            cheapest.Add(vertex, infEdge);
        }

        while (components.Count > 1)
        {
            RefreshCheapest(mst, cheapest, infEdge);

            foreach (var vertex in adjacencyList.Keys)
            {
                foreach (var pair in adjacencyList[vertex])
                {
                    var edge = new Edge(vertex, pair.Key, pair.Value);
                    CheapestEdge(edge, cheapest, components, infEdge);
                }
            }

            foreach (var vertex in mst.Vertices)
            {
                var edge = cheapest[vertex];

                if (!InSameComponent(edge.Vertex1, edge.Vertex2, components)
                    && edge != infEdge && components.Count > 1)
                {
                    UnionGraphs(components, edge, mst);
                }
            }
        }

        return mst;
    }

    public Graph FindMST(int[,] adjacencyMatrix)
    {
        var vertices = new List<Vertex>();
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            vertices.Add(new Vertex(i.ToString()));
        }

        var mst = new Graph(vertices);
        var components = new List<Graph>();
        var cheapest = new Dictionary<Vertex, Edge>();
        var zeroVertex = new Vertex("...");
        var infEdge = new Edge(zeroVertex, zeroVertex, int.MaxValue);

        mst.Vertices.ForEach(v => components.Add(new Graph([v])));

        foreach (var vertex in mst.Vertices)
        {
            cheapest.Add(vertex, infEdge);
        }

        while (components.Count > 1)
        {
            RefreshCheapest(mst, cheapest, infEdge);

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[i, j] != 0)
                    {
                        var edge = new Edge(vertices[i], vertices[j], adjacencyMatrix[i, j]);
                        CheapestEdge(edge, cheapest, components, infEdge);
                    }
                }
            }

            foreach (var vertex in mst.Vertices)
            {
                var edge = cheapest[vertex];

                if (!InSameComponent(edge.Vertex1, edge.Vertex2, components) && edge != infEdge && components.Count > 1)
                {
                    UnionGraphs(components, edge, mst);
                }
            }
        }

        return mst;
    }

    private static bool InSameComponent(Vertex vertex1, Vertex vertex2, List<Graph> components)
    {
        return components.Any(component => component.Adjacent(vertex1, vertex2));
    }

    private static void CheapestEdge(Edge edge, Dictionary<Vertex, Edge> cheapest, List<Graph> components, Edge infEdge)
    {
        if (InSameComponent(edge.Vertex1, edge.Vertex2, components))
            return;
        if (cheapest[edge.Vertex1] == infEdge || cheapest[edge.Vertex2].Weight > edge.Weight)
            cheapest[edge.Vertex1] = edge;
        if (cheapest[edge.Vertex2] == infEdge || cheapest[edge.Vertex1].Weight > edge.Weight)
            cheapest[edge.Vertex2] = edge;
    }

    private void UnionGraphs(List<Graph> components, Edge edge, Graph mst)
    {
        var graph1 = components.Find(c => c.Vertices.Contains(edge.Vertex1));
        var graph2 = components.Find(c => c.Vertices.Contains(edge.Vertex2));
        var unionGraph = new Graph(graph1.Vertices.Union(graph2.Vertices).ToList(),
            graph1.Edges.Union(graph2.Edges).ToList());
        unionGraph.AddEdge(edge);
        components.Remove(graph1);
        components.Remove(graph2);
        components.Add(unionGraph);
        mst.AddEdge(edge);
    }

    private void RefreshCheapest(Graph mst, Dictionary<Vertex, Edge> cheapest, Edge infEdge)
    {
        foreach (var vertex in mst.Vertices)
        {
            cheapest[vertex] = infEdge;
        }
    }
}