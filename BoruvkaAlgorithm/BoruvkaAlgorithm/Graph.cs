namespace BoruvkaAlgorithm;

/// <summary>
/// Represents a vertex in a graph.
/// </summary>
/// <remarks>
/// A vertex is a fundamental part of a graph. It can have a name, 
/// and it can be connected to other vertices via edges. 
/// Each vertex maintains a list of its neighboring vertices and edges.
/// </remarks>

public class Vertex
{
    public string Name { get; set; }
    public List<Vertex>? Neighbors { get; set; }
    public List<Edge>? Edges { get; set; }

    public Vertex(string name)
    {
        Name = name;
        Neighbors = new List<Vertex>();
        Edges = new List<Edge>();
    }

    public Vertex(string name, List<Vertex> neighbors, List<Edge> edges)
    {
        Name = name;
        Neighbors = neighbors;
        Edges = edges;
    }
}

/// <summary>
/// Represents an non-oriented weighed edge in a graph.
/// </summary>
/// <remarks>
/// An edge is a connection between two vertices in a graph.
/// Each edge has a weight associated with it.
/// </remarks>

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
        Vertex1.Neighbors?.Add(Vertex2);
        Vertex2.Neighbors?.Add(Vertex1);
        Vertex1.Edges?.Add(this);
        Vertex2.Edges?.Add(this);
    }
}

public class Graph
{
    private List<Vertex> Vertices { get; }
    private List<Edge> Edges { get; }

    public Graph(Vertex[] vertices, Edge[] edges)
    {
        Vertices = new();
        Edges = new();
        Array.ForEach(vertices, v => Vertices.Add(v));
        Array.ForEach(edges, e => Edges.Add(e));
    }

    public Graph(List<Vertex> vertices, List<Edge> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public Graph(Vertex[] vertices)
    {
        Vertices = new();
        Array.ForEach(vertices, v => Vertices.Add(v));
        Edges = new();
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

    public void AddVertex(Vertex vertex)
    {
        Vertices.Add(vertex);
    }

    public void AddEdge(Edge edge)
    {
        if (!Vertices.Contains(edge.Vertex1) || !Vertices.Contains(edge.Vertex2))
        {
            throw new ArgumentException("The edge must connect two vertices in the graph.");
        }

        Edges.Add(edge);
    }

    public List<Vertex> GetNeighbors(Vertex vertex)
    {
        return vertex.Neighbors ?? new();
    }
}