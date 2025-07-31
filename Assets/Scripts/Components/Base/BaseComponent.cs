using UnityEngine;
using System.Collections.Generic;

public abstract class BaseComponent : MonoBehaviour, ISignalNode
{
    protected SignalData inputSignal;
    protected SignalData outputSignal;

    [SerializeField] protected ConnectorPoint Point1;
    [SerializeField] protected ConnectorPoint Point2;
    
    public abstract float GetResistance();
    public abstract void ProcessSignal(SignalData input);
    public virtual SignalData GetOutputSignal() => outputSignal;
    public virtual SignalData GetInputSignal() => inputSignal;
    
    public ConnectorPoint GetAnotherPoint(ConnectorPoint point)
    {
        if (point == Point1)
            return Point2;
        else if (point == Point2)
        {
            return Point1;
        }

        return null;
    }

    public float GetVoltage()
    {
        return Mathf.Abs(GetInputSignal().Voltage - 
                  GetOutputSignal().Voltage);
    }
    
}

