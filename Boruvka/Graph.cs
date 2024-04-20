namespace Boruvka_Algorithm;

public class Vertex
{
    public string Name { get; set; }
    internal List<Vertex> Neighbors { get; }
    internal List<Edge> Edges { get; }

    public Vertex(string name)
    {
        Name = name;
        Neighbors = new List<Vertex>();
        Edges = new List<Edge>();
    }
}

public class Edge
{
    public Vertex Vertex1 { get; set; }
    public Vertex Vertex2 { get; set; }
    public int Weight { get; set; }

    public Edge(Vertex ver1, Vertex ver2, int weight)
    {
        Vertex1 = ver1;
        Vertex2 = ver2;
        Weight = weight;

        AssignVertices();
    }

    private void AssignVertices()
    { 
        Vertex1.Neighbors.Add(Vertex2);
        Vertex2.Neighbors.Add(Vertex1);
        Vertex1.Edges.Add(this);
        Vertex2.Edges.Add(this);
    }
}

public class Graph
{
    public List<Vertex> Vertices { get; }
    public List<Edge> Edges { get; }


    public Graph(Graph graph1, Graph graph2)
    {
        Vertices = new List<Vertex>();
        Edges = new List<Edge>();
        graph1.Vertices.ForEach(v => Vertices.Add(v));
        graph2.Vertices.ForEach(v => Vertices.Add(v));
        graph1.Edges.ForEach(e => Edges.Add(e));
        graph2.Edges.ForEach(e => Edges.Add(e));
    }

    public Graph(List<Vertex> vertices, List<Edge> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public Graph(List<Vertex> vertices)
    {
        Vertices = vertices;
        Edges = new();
    }

    public Graph()
    {
        Vertices = new();
        Edges = new();
    }

    public bool Adjacent(Vertex vertex1, Vertex vertex2)
    {
        foreach (var edge in Edges)
        {
            if ((edge.Vertex1 == vertex1 && edge.Vertex2 == vertex2) ||
                (edge.Vertex1 == vertex2 && edge.Vertex2 == vertex1))
            {
                return true;
            }
        }

        return false;
    }

    public List<Vertex> GetNeighbors(Vertex vertex)
    {
        return vertex.Neighbors;
    }

    public void AddVertex(Vertex vertex)
    {
        if (Vertices.Contains(vertex)) return;
        Console.WriteLine("Error. Vertex already exists in the graph.");
    }

    public void RemoveVertex(Vertex vertex)
    {
        if (!Vertices.Contains(vertex))
        {
            Console.WriteLine("Error. Vertex does not exist in the graph.");
            return;
        }

        foreach (var edge in Edges)
        {
            if (edge.Vertex1 == vertex || edge.Vertex2 == vertex)
            {
                Edges.Remove(edge);
            }
        }

        Vertices.Remove(vertex);
    }

    public void AddEdge(Edge edge)
    {
        if (!Vertices.Contains(edge.Vertex1) || !Vertices.Contains(edge.Vertex2))
        {
            Console.WriteLine("Error. One or both vertices are not in the graph.");
            return;
        }

        if (Edges.Any(e => (e.Vertex1 == edge.Vertex1 && e.Vertex2 == edge.Vertex2) ||
                           (e.Vertex1 == edge.Vertex2 && e.Vertex2 == edge.Vertex1)))
        {
            return;
        }

        Edges.Add(edge);
    }

    public void DeleteEdge(Edge edge)
    {
        if (!Edges.Contains(edge))
        {
            Console.WriteLine("Error. Edge is not in the graph.");
            return;
        }

        Edges.Remove(edge);
    }

    public Dictionary<Vertex, List<KeyValuePair<Vertex, int>>> AdjacencyList()
    {
        var adjacencyList = new Dictionary<Vertex, List<KeyValuePair<Vertex, int>>>();
        foreach (var vertex in Vertices)
        {
            var neighbors = new List<KeyValuePair<Vertex, int>>();
            foreach (var edge in Edges)
            {
                if (edge.Vertex1 == vertex)
                {
                    neighbors.Add(new KeyValuePair<Vertex, int>(edge.Vertex2, edge.Weight));
                }
                else if (edge.Vertex2 == vertex)
                {
                    neighbors.Add(new KeyValuePair<Vertex, int>(edge.Vertex1, edge.Weight));
                }
            }

            adjacencyList[vertex] = neighbors;
        }

        return adjacencyList;
    }


    public int[,] AdjacencyMatrix()
    {
        var adjacencyMatrix = new int[Vertices.Count, Vertices.Count];
        for (var i = 0; i < Vertices.Count; i++)
        {
            for (var j = 0; j < Vertices.Count; j++)
            {
                adjacencyMatrix[i, j] = FindEdgeWeight(Vertices[i], Vertices[j]);
            }
        }

        return adjacencyMatrix;
    }

    private int FindEdgeWeight(Vertex vertex1, Vertex vertex2)
    {
        foreach (var edge in Edges)
        {
            if ((edge.Vertex1 == vertex1 && edge.Vertex2 == vertex2) ||
                (edge.Vertex1 == vertex2 && edge.Vertex2 == vertex1))
            {
                return edge.Weight;
            }
        }

        return 0;
    }
}