using UnityEngine;

public class ResistorComponent : BaseComponent
{
    [SerializeField] private float resistance = 10f;

    public override float GetResistance() => resistance;

    public override void ProcessSignal(SignalData input)
    {
        float voltageDrop = input.Current * resistance;
        float outVoltage = input.Voltage - voltageDrop;

        float inV = Mathf.Round(input.Voltage * 10000f) / 10000f;
        float curr = Mathf.Round(input.Current * 10000f) / 10000f;
        float outV = Mathf.Round(outVoltage * 10000f) / 10000f;

        outputSignal = new SignalData(outVoltage, input.Current);

        Debug.Log($"[Resistor] Получено: {inV}В, {curr}А → Выход: {outV}В");
    }
}

