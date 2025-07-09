using UnityEngine;

public class ResistorComponent : BaseComponent
{
    [SerializeField] private float resistance = 10f;

    public override float GetResistance() => resistance;

    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;

        float voltageDrop = input.Current * resistance;
        float outVoltage = input.Voltage - voltageDrop;

        outputSignal = new SignalData(outVoltage, input.Current);

        Debug.Log($"[Resistor] Получено: {input.Voltage}В, {input.Current}А → Выход: {outVoltage}В");
    }
}

