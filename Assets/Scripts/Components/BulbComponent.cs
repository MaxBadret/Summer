using System;
using UnityEngine;

public class BulbComponent : BaseComponent
{
    [Header("Sprites")]
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    
    [Header("Electrical")]
    [SerializeField] private float resistance = 20f;
    [SerializeField] private float minVoltage = 2f; // Порог включения

    private SpriteRenderer spriteRenderer;
    private bool isPowered = false;
    private CircuitManager circuit;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = offSprite; // Начальное состояние
        circuit = GameObject.Find("CircuitManager").GetComponent<CircuitManager>();
    }

    public override float GetResistance() => resistance;

    public override void ProcessSignal(SignalData input)
    {
        inputSignal = input;

        // Меняем спрайт только при изменении состояния
        if (input.Voltage >= minVoltage)
        {
            spriteRenderer.sprite = onSprite;
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }

        outputSignal = input; // Пропускаем сигнал без изменений
    }

    private void Update()
    {
        if (Point1.ConnectedPoints.Count == 0 || Point2.ConnectedPoints.Count == 0 || !circuit.circuitValid)
        {
            spriteRenderer.sprite = offSprite;
        }
    }
}


