using UnityEngine;
using System.Collections.Generic;

public class PowerSourceComponent : BaseComponent
{
    [SerializeField] private float voltage = 5f; // Напряжение источника
    [SerializeField] private float internalResistance = 0.1f;

    // Плюсовой и минусовой выходы
    [SerializeField] private BaseComponent positiveOutput;
    [SerializeField] private BaseComponent negativeOutput;

    public override float GetResistance() => internalResistance;

    public override List<ISignalNode> GetOutputs()
    {
        return new List<ISignalNode> { positiveOutput };
    }

    public override List<ISignalNode> GetInputs()
    {
        return new List<ISignalNode> { negativeOutput }; // GND
    }

    public override void ProcessSignal(SignalData input)
    {
        // Источник ничего не обрабатывает — он и есть начало
        outputSignal = new SignalData(voltage, input.Current); // пока просто передаём ток дальше
    }

    public override SignalData GetOutputSignal()
    {
        return outputSignal;
    }

    public void SetConnections(BaseComponent positive, BaseComponent negative)
    {
        positiveOutput = positive;
        negativeOutput = negative;
        ConnectOutput(positive);
        ConnectInput(negative);
    }
}
