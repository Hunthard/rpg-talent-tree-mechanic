using System.Collections.Generic;

public class Node
{
    public int Id { get; private set; }

    public HashSet<Node> Nodes { get; private set; }

    public Node()
    {
        Nodes = new HashSet<Node>();
    }

    public Node(params Node[] nodes) : this()
    {
        foreach (var node in nodes)
        {
            Nodes.Add(node);
        }
    }
}
