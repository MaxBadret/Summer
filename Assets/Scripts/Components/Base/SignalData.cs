using UnityEngine;

public struct SignalData
{
    public float Voltage;
    public float Current;

    public SignalData(float voltage, float current)
    {
        Voltage = voltage; //напряжение
        Current = current; //сила тока
    }
}

