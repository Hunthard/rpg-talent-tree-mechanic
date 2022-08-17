using System.Collections.Generic;

public abstract class Graph<T>
{
    public HashSet<T> Nodes { get; private set; }

    private T _rootNode;

    public Graph()
    {
        Nodes = new HashSet<T>();
    }

    public Graph(IList<T> nodes) : this()
    {
        foreach (var node in nodes)
        {
            Nodes.Add(node);
        }
    }

    abstract public void ResetState();
}
