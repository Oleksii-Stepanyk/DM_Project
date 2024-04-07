namespace BoruvkaAlgorithm;

public class Vertex
{
    public string Name { get; set; }
    internal HashSet<Vertex> Neighbors { get; }
    internal HashSet<Edge> Edges { get; }

    public Vertex(string name)
    {
        Name = name;
        Neighbors = new HashSet<Vertex>();
        Edges = new HashSet<Edge>();
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
        if (!Vertex1.Neighbors.Add(Vertex2))
        {
            return;
        }

        Vertex2.Neighbors.Add(Vertex1);
        Vertex1.Edges.Add(this);
        Vertex2.Edges.Add(this);
    }
}

public class Graph
{
    public HashSet<Vertex> Vertices { get; }
    public HashSet<Edge> Edges { get; }

    public Graph(HashSet<Vertex> vertices, HashSet<Edge> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public Graph(HashSet<Vertex> vertices)
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

    public HashSet<Vertex> GetNeighbors(Vertex vertex)
    {
        return vertex.Neighbors;
    }

    public void AddVertex(Vertex vertex)
    {
        if (Vertices.Add(vertex)) return;
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

    public List<List<Vertex>> AdjacencyList()
    {
        var adjacencyList = new List<List<Vertex>>();
        foreach (var vertex in Vertices)
        {
            var neighbors = new List<Vertex>();
            foreach (var neighbor in vertex.Neighbors)
            {
                neighbors.Add(neighbor);
            }

            adjacencyList.Add(neighbors);
        }

        return adjacencyList;
    }


    public int[,] AdjacencyMatrix()
    {
        var adjacencyMatrix = new int[Vertices.Count, Vertices.Count];
        var vertexList = Vertices.ToList();
        for (var i = 0; i < Vertices.Count; i++)
        {
            for (var j = 0; j < Vertices.Count; j++)
            {
                adjacencyMatrix[i, j] = FindEdgeWeight(vertexList[i], vertexList[j]);
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