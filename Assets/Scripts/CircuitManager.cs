using UnityEngine;
using System.Collections.Generic;

public class CircuitManager : MonoBehaviour
{
    public static CircuitManager Instance { get; private set; }

    [SerializeField] private PowerSourceComponent powerSource;

    private BaseComponent lastBeforePowerSource = null;
    private List<BaseComponent> lastPath = new();
    private bool circuitChanged = true;

    private bool circuitValid = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        circuitChanged = false;
    }

    void Update()
    {
        // Только один раз на изменение схемы
        if (circuitChanged)
        {
            Debug.Log("🔄 Схема изменилась, проверяем...");

            List<BaseComponent> path = BuildPath(powerSource.PositiveOutput);

            // Сбрасываем circuitChanged сразу после построения пути,
            // чтобы не повторять этот процесс при следующем кадре
            circuitChanged = false;

            if (!IsCircuitClosed(powerSource))
            {
                Debug.Log("❌ Цепь не замкнута! Добавь соединения.");
                circuitValid = false;
                return;
            }

            lastPath = path;
            circuitValid = true;

            Debug.Log("✅ Цепь замкнута. Запуск симуляции.");
        }

        // Постоянная симуляция — только если цепь валидна
        if (circuitValid)
        {
            SimulateCircuit(lastPath);
        }
    }

    public void MarkCircuitChanged()
    {
        circuitChanged = true;
    }


    private void SimulateCircuit(List<BaseComponent> path)
    {
        float totalResistance = CalculateTotalResistance(path);
        totalResistance = Mathf.Round(totalResistance * 10000f) / 10000f;
        Debug.Log($"🔍 Общее сопротивление: {totalResistance} Ом");

        float current = powerSource.GetVoltage() / totalResistance;
        current = Mathf.Round(current * 10000f) / 10000f;
        Debug.Log($"⚡ Ток в цепи: {current} А");

        SignalData signal = new SignalData(powerSource.GetVoltage(), current);

        foreach (BaseComponent component in path)
        {
            component.ProcessSignal(signal);
            signal = component.GetOutputSignal();
        }
    }

    private List<BaseComponent> BuildPath(ConnectorPoint start)
    {
        List<BaseComponent> path = new();
        HashSet<BaseComponent> visited = new();
        Traverse(start, path, visited);
        return path;
    }

    private void Traverse(ConnectorPoint point, List<BaseComponent> path, HashSet<BaseComponent> visited)
    {
        if (point.OwnerComponent== null || visited.Contains(point.OwnerComponent)) return;
        visited.Add(point.OwnerComponent);
        path.Add(point.OwnerComponent);

        foreach (ConnectorPoint next in point.ConnectedPoints)
        {
            if (next != null)
            {
                var owner = next.OwnerComponent;
                if (owner.GetAnotherPoint(next))
                    Traverse(owner.GetAnotherPoint(next), path, visited);
            }
        }
    }

    private float CalculateTotalResistance(List<BaseComponent> components)
    {
        float total = 0f;
        foreach (BaseComponent c in components)
        {
            total += c.GetResistance();
        }
        return total;
    }

    private bool IsCircuitClosed(PowerSourceComponent source)
    {
        HashSet<BaseComponent> visited = new();
        return SearchLoop(source.PositiveOutput, source.NegativeInput, visited);
    }
    
    private bool SearchLoop(ConnectorPoint current, ConnectorPoint  target, HashSet<BaseComponent> visited)
    {
        if (visited.Contains(current.OwnerComponent)) return false;
        visited.Add(current.OwnerComponent);

        foreach (ConnectorPoint next in current.ConnectedPoints)
        {
            var owner = next.OwnerComponent;
            var outputPoint = owner.GetAnotherPoint(next);
            if (next == target)
            {
                lastBeforePowerSource = current.OwnerComponent;
                return true;
            }
            if (SearchLoop(outputPoint, target, visited))
                return true;
        }

        return false;
    }
    
    public BaseComponent GetLastComponent()
    {
        return lastBeforePowerSource;
    }
}
