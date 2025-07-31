using TMPro;
using UnityEngine;

public class AmperComponent : BaseComponent
{
    [SerializeField] private TextMeshPro displayText; // Для отображения значения
    private float current;
    
    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;
        current = input.Current;
        UpdateDisplay();
        Debug.Log($"Прошел через амперметр");
        // Пропускаем сигнал дальше без изменений
        outputSignal = new SignalData(input.Voltage, input.Current);
    }

    private void UpdateDisplay()
    {
        // Округляем до 2 знаков и добавляем единицы измерения
        displayText.text = $"{Mathf.Round(current * 100) / 100} A";
    }

    public override float GetResistance()
    {
        // Идеальный амперметр имеет нулевое сопротивление
        return 0.001f; // Практически 0 Ом
    }
}


