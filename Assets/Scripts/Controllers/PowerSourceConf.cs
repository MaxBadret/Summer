using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerSourceConf : MonoBehaviour
{
    public static PowerSourceConf Instance;

    [SerializeField] PowerSourceComponent pwSource;
    [SerializeField] TMP_InputField inpField;

    [SerializeField] TMP_InputField outVolt;
    [SerializeField] TMP_InputField outAmper;
    [SerializeField] TMP_InputField totalResist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        inpField.text = pwSource.GetVoltage2().ToString("F4");
    }

    void Update()
    {
        if (CircuitManager.Instance == null)
            return;

        var last = CircuitManager.Instance.GetLastComponent();
        if (last == null)
            return;

        SignalData signal = last.GetOutputSignal();

        outVolt.text = signal.Voltage.ToString("F4");
        outAmper.text = signal.Current.ToString("F4");
        totalResist.text = CircuitManager.Instance.totRes.ToString("F4");
    }

    public void ApplyValue()
    {
        FormatAndApplyInput();

        if (pwSource != null && float.TryParse(inpField.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float voltage))
        {
            pwSource.SetVoltage(voltage);
        }
    }

    public void FormatAndApplyInput()
    {
        if (inpField == null) return;

        string input = inpField.text;

        // 1. Удаляем все символы, кроме цифр и точек/запятых
        string filtered = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '.' || c == ',')
            {
                filtered += c;
            }
        }

        // 2. Заменяем все запятые на точки
        filtered = filtered.Replace('.', ',');

        // 3. Удаляем минусы (делаем число положительным)
        filtered = filtered.Replace("-", "");

        // 4. Ограничиваем длину строки до 10 символов
        if (filtered.Length > 10)
        {
            filtered = filtered.Substring(0, 10);
        }

        // 5. Обновляем текст в поле
        inpField.text = filtered;
    }

    private float GetOutVoltage()
    {
        // 🔌 Проверка выхода схемы на нужное напряжение
        var last = CircuitManager.Instance.GetLastComponent();
        if (last == null)
            return 0;
        SignalData signal = last.GetOutputSignal();

        return signal.Voltage;
    }

    private float GetOutAmper()
    {
        // 🔌 Проверка выхода схемы на нужное напряжение
        var last = CircuitManager.Instance.GetLastComponent();
        if (last == null)
            return 0;
        SignalData signal = last.GetOutputSignal();

        return signal.Current;
    }
}
