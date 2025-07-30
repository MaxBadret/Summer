using UnityEngine;
using System.Collections.Generic;

public class CircuitManager : MonoBehaviour
{
    public static CircuitManager Instance { get; private set; }

    [SerializeField] private PowerSourceComponent powerSource;

    private List<BaseComponent> lastPath = new();
    private bool circuitChanged = true;

    private bool circuitValid = false;

    private BaseComponent lastBeforePowerSource = null;

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

            List<BaseComponent> path = BuildPath(powerSource);

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

    private List<BaseComponent> BuildPath(BaseComponent start)
    {
        List<BaseComponent> path = new();
        HashSet<BaseComponent> visited = new();
        Traverse(start, path, visited);
        return path;
    }

    private void Traverse(BaseComponent node, List<BaseComponent> path, HashSet<BaseComponent> visited)
    {
        if (node == null || visited.Contains(node)) return;
        visited.Add(node);
        path.Add(node);

        foreach (BaseComponent next in node.GetOutputs())
        {
            if (next != null)
                Traverse(next, path, visited);
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
        return SearchLoop(source, source, visited);
    }

    private bool SearchLoop(BaseComponent current, BaseComponent target, HashSet<BaseComponent> visited)
    {
        if (visited.Contains(current)) return false;
        visited.Add(current);

        foreach (BaseComponent next in current.GetOutputs())
        {
            if (next == target)
            {
                lastBeforePowerSource = current; // ✅ Запоминаем последний перед источником
                return true;
            }

            if (SearchLoop(next, target, visited))
                return true;
        }

        return false;
    }
    
    public BaseComponent GetLastComponent()
    {
        return lastBeforePowerSource;
    }
}
