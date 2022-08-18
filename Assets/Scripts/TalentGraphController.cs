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

    private void Print(int index) => Debug.Log(index);

    private void LogGraph()
    {
        _graph.BFS(0, null, Print);
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
            _graph.AddNode(new Talent(i));
        }
    }

    private void AddLinksToGraph()
    {
        _graph.AddLink(0, 1);
        _graph.AddLink(0, 4);
        _graph.AddLink(0, 2);
        _graph.AddLink(0, 9);
        _graph.AddLink(0, 8);
        _graph.AddLink(4, 5);
        _graph.AddLink(4, 6);
        _graph.AddLink(5, 7);
        _graph.AddLink(6, 7);
        _graph.AddLink(2, 3);
        _graph.AddLink(8, 10);
        _graph.AddLink(9, 10);
    }
}
