using UnityEngine;

public class Switch : BaseComponent
{
    [SerializeField] private bool isOn;
    [SerializeField] private float resistance = 0f;
    private bool isMouseOver = false;

    public override float GetResistance()
    {
        return isOn ? 0f : 1e6f; // 0 Ом при ON, 1 МОм при OFF
    }

    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;
        if (isOn)
        {
            // Переключатель замкнут — передаём сигнал дальше
            outputSignal = new SignalData(input.Voltage, input.Current);
            Debug.Log("[Switch] ON → Ток идёт.");
        }
        else
        {
            // Переключатель разомкнут — ток не идёт
            outputSignal = new SignalData(0, 0);
            Debug.Log("[Switch] OFF.");
        }
    }

    // Метод для переключения состояния (можно вызывать через UI или клавишу)
    public void Toggle()
    {
        isOn = !isOn;
        Debug.Log("Переключатель: " + (isOn ? "ON" : "OFF"));
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
            Toggle();
        }
    }
}


