using TMPro;
using UnityEngine;

public class VoltMeterComponent : BaseComponent
{
    [SerializeField] private TextMeshPro displayText; // Для отображения значения
    private float current;
    [SerializeField] private ConnectorPoint LeftPoint;
    [SerializeField] private ConnectorPoint RightPoint;
    
    
    public override float GetResistance()
    {
        return 1e9f; // Огромное сопротивление (идеальный вольтметр)
    }

    public override void ProcessSignal(SignalData input)
    {
        
    }
    
    
}


