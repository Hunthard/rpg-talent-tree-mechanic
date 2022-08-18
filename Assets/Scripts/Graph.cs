using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> where T : Node
{
    public List<T> Nodes { get; private set; }

    private LinkedList<int>[] _adjacencyMatrix;

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

    public void BFS(int nodeId, Predicate<Node> condition = null, Action<int> fallback = null)
    {
        var explored = new bool[_size];
        var nodeQueue = new Queue<int>();

        explored[nodeId] = true;
        nodeQueue.Enqueue(nodeId);

        while (nodeQueue.Count > 0)
        {
            var node = nodeQueue.Dequeue();
            fallback(node);

            foreach (var edge in _adjacencyMatrix[node])
            {
                if (explored[edge]) continue;

                explored[edge] = true;
                nodeQueue.Enqueue(edge);
            }
        }
    }
}
