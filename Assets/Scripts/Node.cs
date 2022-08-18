using System;

public class Node : IEquatable<Node>
{
    public int Id { get; private set; }

    private Node()
    { }

    public Node(int id) : this()
    {
        Id = id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Node);
    }

    public bool Equals(Node other)
    {
        return other != null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
