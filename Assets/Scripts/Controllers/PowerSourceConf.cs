using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerSourceConf : MonoBehaviour
{
    public static PowerSourceConf Instance;

    [SerializeField] PowerSourceComponent pwSource;
    [SerializeField] TMP_InputField inpField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        inpField.text = pwSource.GetVoltage2().ToString("F4");
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
}
