using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGraphController : MonoBehaviour
{
    private TalentGraph _graph;

    [ContextMenu("Test")]
    public void Foo()
    {
        InitGraph();
        LogGraph();
    }

    private void Print(Node node) => Debug.Log(node.Id);

    private bool IsRoot(Talent node) => _graph.IsRoot(node) || true;

    private void LogGraph()
    {
        Predicate<Talent> isRoot = IsRoot;
        
        _graph.BFS(100, isRoot, Print);
        Debug.Log("");
        _graph.DFS(100, isRoot, Print);
    }

    private void InitGraph()
    {
        _graph = new TalentGraph(11);

        AddNodesToGraph();
        AddLinksToGraph();
    }

    private void AddNodesToGraph()
    {
        for (int i = 0; i < 11; i++)
        {
            _graph.AddNode(new Talent(100 + i));
        }
    }

    private void AddLinksToGraph()
    {
        _graph.AddLink(100 + 0, 100 + 1);
        _graph.AddLink(100 + 0, 100 + 4);
        _graph.AddLink(100 + 0, 100 + 2);
        _graph.AddLink(100 + 0, 100 + 9);
        _graph.AddLink(100 + 0, 100 + 8);
        _graph.AddLink(100 + 4, 100 + 5);
        _graph.AddLink(100 + 4, 100 + 6);
        _graph.AddLink(100 + 5, 100 + 7);
        _graph.AddLink(100 + 6, 100 + 7);
        _graph.AddLink(100 + 2, 100 + 3);
        _graph.AddLink(100 + 8, 100 + 10);
        _graph.AddLink(100 + 9, 100 + 10);
    }
}
