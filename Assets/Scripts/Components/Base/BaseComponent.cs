using UnityEngine;
using System.Collections.Generic;

public abstract class BaseComponent : MonoBehaviour, ISignalNode
{
    protected SignalData inputSignal;
    protected SignalData outputSignal;

    [SerializeField] protected List<BaseComponent> inputComponents = new();
    [SerializeField] protected List<BaseComponent> outputComponents = new();

    public virtual List<ISignalNode> GetInputs() => new List<ISignalNode>(inputComponents);
    public virtual List<ISignalNode> GetOutputs() => new List<ISignalNode>(outputComponents);

    public abstract float GetResistance();
    public abstract void ProcessSignal(SignalData input);
    public virtual SignalData GetOutputSignal() => outputSignal;

    public void ConnectInput(BaseComponent component)
    {
        if (!inputComponents.Contains(component))
            inputComponents.Add(component);
    }

    public void ConnectOutput(BaseComponent component)
    {
        if (!outputComponents.Contains(component))
            outputComponents.Add(component);
    }
}

