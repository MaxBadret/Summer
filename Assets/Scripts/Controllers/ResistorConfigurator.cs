using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ResistorConfigurator : MonoBehaviour
{
    public static ResistorConfigurator Instance;

    [Header("UI")]
    [SerializeField] private GameObject changeResPan;
    private TMP_InputField resistanceInput;
    [SerializeField] private Camera uiCamera;

    private ResistorComponent targetResistor;

    void Start()
    {
        resistanceInput = changeResPan.GetComponent<TMP_InputField>();
        Instance = this;
        changeResPan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenConfig(ResistorComponent resistor)
    {
        targetResistor = resistor;
        resistanceInput.text = resistor.GetResistance().ToString("F4");
        changeResPan.SetActive(true);

        Vector2 screenPos = Camera.main.WorldToScreenPoint(resistor.transform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            changeResPan.transform.parent as RectTransform, // родитель должен быть UI-объектом
            screenPos,
            uiCamera, // если Canvas в Screen Space - Camera. Если Overlay — ставь null
            out Vector2 localPoint
        );

        localPoint.y += 60f;

        changeResPan.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    public void ApplyValue()
    {
        FormatAndApplyInput();

        if (targetResistor != null && float.TryParse(resistanceInput.text, out float resistance))
        {
            targetResistor.SetResistance(resistance);
        }
    }


    public void CloseConfig()
    {
        targetResistor = null;
        changeResPan.SetActive(false);
    }

    public void FormatAndApplyInput()
    {
        if (resistanceInput == null) return;

        string input = resistanceInput.text;

        // 1. Удаляем все символы, кроме цифр и точек/запятых
        string filtered = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '.' || c == ',')
            {
                filtered += c;
            }
        }

        // 2. Заменяем все точки на запятые
        filtered = filtered.Replace('.', ',');

        // 3. Удаляем минусы (делаем число положительным)
        filtered = filtered.Replace("-", "");

        // 4. Ограничиваем длину строки до 10 символов
        if (filtered.Length > 10)
        {
            filtered = filtered.Substring(0, 10);
        }

        // 5. Обновляем текст в поле
        resistanceInput.text = filtered;
    }
}
