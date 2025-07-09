using System.Collections.Generic;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    [SerializeField] private BaseComponent Power;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
    
    private List<ISignalNode> BuildPath(ISignalNode start)
    {
        List<ISignalNode> path = new();
        HashSet<ISignalNode> visited = new();
        Traverse(start, path, visited);
        return path;
    }
    
    void Traverse(ISignalNode node, List<ISignalNode> path, HashSet<ISignalNode> visited)
    {
        if (visited.Contains(node)) return;
        visited.Add(node);
        path.Add(node);

        foreach (ISignalNode next in node.GetOutputs())
        {
            Traverse(next, path, visited);
        }
    }
}
