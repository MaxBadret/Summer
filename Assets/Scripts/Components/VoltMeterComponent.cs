using TMPro;
using UnityEngine;

public class VoltMeterComponent : BaseComponent
{
    [SerializeField] private TextMeshPro displayText; // Для отображения значения
    private float current;
    private float measuredVoltage;
    
    
    private void Update()
    {
        if (Point1.ConnectedPoints.Count > 0 && Point2.ConnectedPoints.Count > 0)
        {
            MeasureVoltage();
        }
    }

    private void MeasureVoltage()
    {
        if (Point1.ConnectedPoints.Count == 0 || Point2.ConnectedPoints.Count == 0)
        {
            measuredVoltage = 0;
        }
        else
        {
            var conectedComponentToPoint1 = Point1.ConnectedPoints[0].OwnerComponent;
            var conectedComponentToPoint2 = Point2.ConnectedPoints[0].OwnerComponent;
            if (conectedComponentToPoint1 == conectedComponentToPoint2)
            {
                measuredVoltage = Mathf.Abs(conectedComponentToPoint1.GetVoltage());
            }
        }
        
        //measuredVoltage = Mathf.Abs(voltage1 - voltage2);
        displayText.text = $"{measuredVoltage:F2} V";
    }

    private float GetVoltageAtPoint(ConnectorPoint point)
    {
        // Находим компонент, подключенный к этой точке
        if (point.ConnectedPoints.Count == 0) return 0;
        
        var connectedComponent = point.ConnectedPoints[0].OwnerComponent;
        return connectedComponent.GetOutputSignal().Voltage;
    }

    public override float GetResistance()
    {
        // Очень высокое сопротивление, чтобы не влиять на цепь
        return 0;
    }

    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;
    }
}


