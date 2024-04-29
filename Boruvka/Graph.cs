namespace Boruvka_Algorithm;

public class Vertex
{
    public string Name { get; set; }
    internal List<Vertex> Neighbors { get; }

    public Vertex(string name)
    {
        Name = name;
        Neighbors = new List<Vertex>();
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
    
    public Dictionary<Vertex, List<KeyValuePair<Vertex, int>>> MatrixToList(int[,] adjacencyMatrix)
    {
        var adjacencyList = new Dictionary<Vertex, List<KeyValuePair<Vertex, int>>>();
        var vertices = new List<Vertex>();
        foreach (var vertex in adjacencyMatrix)
        {
            vertices.Add(new Vertex(vertex.ToString()));
        }

        var numberOfVertices = vertices.Count;

        for (int i = 0; i < numberOfVertices; i++)
        {
            var neighbors = new List<KeyValuePair<Vertex, int>>();
            
            for (int j = 0; j < numberOfVertices; j++)
            {
                if (adjacencyMatrix[i, j] != 0)
                {
                    neighbors.Add(new KeyValuePair<Vertex, int>(vertices[j], adjacencyMatrix[i, j]));
                }
            }
            adjacencyList[vertices[i]] = neighbors;
        }

        return adjacencyList;
    }

    public int[,] ListToMatrix(Dictionary<Vertex, List<KeyValuePair<Vertex, int>>> adjacencyList)
    {
        var vertices = adjacencyList.Keys.ToList();
        var adjacencyMatrix = new int[vertices.Count, vertices.Count];

        foreach (var vertex in adjacencyList.Keys)
        {
            var i = vertices.IndexOf(vertex);
            foreach (var neighbor in adjacencyList[vertex])
            {
                var j = vertices.IndexOf(neighbor.Key);
                adjacencyMatrix[i, j] = neighbor.Value;
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