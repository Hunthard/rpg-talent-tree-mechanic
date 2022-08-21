using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> where T : Node
{
    public List<T> Nodes { get; private set; }

    protected LinkedList<int>[] _adjacencyMatrix;

    private int _size;

    public Graph(int NodeCount)
    {
        _size = NodeCount;

        _adjacencyMatrix = new LinkedList<int>[NodeCount];
        for (int i = 0; i < _adjacencyMatrix.Length; i++)
        {
            _adjacencyMatrix[i] = new LinkedList<int>();
        }

        Nodes = new List<T>(_size);
    }

    public void AddNode(T node)
    {
        if (Nodes.Contains(node)) return;

        Nodes.Add(node);
    }

    public void AddLink(int idFrom, int idTo)
    {
        var fromIndex = Nodes.FindIndex(node => node.Id == idFrom);
        var toIndex = Nodes.FindIndex(node => node.Id == idTo);

        if (fromIndex == -1 || toIndex == -1) return;

        _adjacencyMatrix[fromIndex].AddLast(toIndex);
    }

    public void BFS(int nodeId, Predicate<T> condition = null, Action<T> fallback = null)
    {
        var explored = new bool[_size];
        var nodeQueue = new Queue<int>();

        var nodeIndex = Nodes.FindIndex(node => node.Id == nodeId);
        try
        {
            explored[nodeIndex] = true;
            nodeQueue.Enqueue(nodeIndex);

            while (nodeQueue.Count > 0)
            {
                nodeIndex = nodeQueue.Dequeue();
                if (condition(Nodes[nodeIndex])) fallback(Nodes[nodeIndex]);

                foreach (var edge in _adjacencyMatrix[nodeIndex])
                {
                    if (explored[edge]) continue;

                    explored[edge] = true;
                    nodeQueue.Enqueue(edge);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }

    public bool IsRoot(T node)
    {
        var index = Nodes.IndexOf(node);

        if (index != 0) return false;

        return true;
    }
}
