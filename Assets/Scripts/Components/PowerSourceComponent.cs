using System;
using UnityEngine;
using System.Collections.Generic;

public class PowerSourceComponent : BaseComponent
{
    [SerializeField] private float voltage = 5f; // Напряжение источника
    [SerializeField] private float internalResistance = 0.1f;

    // Плюсовой и минусовой выходы
    public ConnectorPoint PositiveOutput { get; private set; }
    public ConnectorPoint NegativeInput { get; private set; }

    private void Start()
    {
        PositiveOutput = Point1;
        NegativeInput = Point2;
    }

    public override float GetResistance() => internalResistance;

    /*public override List<ISignalNode> GetOutputs()
    {
        return new List<ISignalNode> { positiveOutput };
    }*/

    /*public override List<ISignalNode> GetInputs()
    {
        return new List<ISignalNode> { negativeOutput }; // GND
    }*/

    public override void ProcessSignal(SignalData input)
    {
        // Источник ничего не обрабатывает — он и есть начало
        float curr = Mathf.Round(input.Current * 10000f) / 10000f;
        outputSignal = new SignalData(voltage, input.Current); // пока просто передаём ток дальше
        Debug.Log($"[PowerSource] Даю напряжение: {voltage}В");
    }

    public override SignalData GetOutputSignal()
    {
        return outputSignal;
    }

    // public void SetConnections(BaseComponent positive, BaseComponent negative)
    // {
    //     PositiveOutput = positive;
    //     NegativeInput = negative;
    //     ConnectOutput(positive);
    //     ConnectInput(negative);
    // }

    public float GetVoltage() => voltage;
}
