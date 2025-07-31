using UnityEngine;

public class DiodeComponent : BaseComponent
{
    [SerializeField] private float forwardVoltageDrop = 0.7f; // Падение напряжения в открытом состоянии
    [SerializeField] private float reverseLeakageCurrent = 0.0001f; // Ток утечки в обратном направлении (необязательно)
    [SerializeField] private bool isForwardBiased; // Для визуализации состояния
    private bool isMouseOver = false;

    public override float GetResistance()
    {
        // В идеале, сопротивление зависит от направления тока
        // Упрощённо: если диод открыт — маленькое сопротивление, если закрыт — огромное
        return isForwardBiased ? 0.01f : 1000000f; 
    }

    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;
        isForwardBiased = input.Voltage >= forwardVoltageDrop;

        if (isForwardBiased)
        {
            // Диод открыт: пропускаем ток с падением напряжения
            float outVoltage = input.Voltage - forwardVoltageDrop;
            outputSignal = new SignalData(outVoltage, input.Current);
            Debug.Log($"[Diode] Открыт! Вход: {input.Voltage}В → Выход: {outVoltage}В");
        }
        else
        {
            // Диод закрыт: ток почти нулевой (утечка)
            outputSignal = new SignalData(0, reverseLeakageCurrent);
            Debug.Log($"[Diode] Закрыт! Почти нет тока.");
        }
    }
    
    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void Update()
    {
        if (isMouseOver && Input.GetKeyDown(KeyCode.Space))
        {
            isForwardBiased = !isForwardBiased;
        }
    }
}

