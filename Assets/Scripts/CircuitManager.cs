using System.Collections.Generic;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    [SerializeField] private BaseComponent Power;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var z = BuildPath(Power);
            foreach (var VARIABLE in z)
            {
                Debug.Log(VARIABLE);
            }
        }
    }
    
    private List<ISignalNode> BuildPath(BaseComponent start)
    {
        List<ISignalNode> path = new();
        HashSet<ISignalNode> visited = new();
        Traverse(start, path, visited);
        return path;
    }
    
    void Traverse(BaseComponent node, List<ISignalNode> path, HashSet<ISignalNode> visited)
    {
        if (visited.Contains(node)) return;
        visited.Add(node);
        path.Add(node);

        foreach (BaseComponent next in node.GetOutputs())
        {
            Traverse(next, path, visited);
        }
    }
}
