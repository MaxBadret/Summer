using UnityEngine;
using System.Collections.Generic;

public interface ISignalNode
{
    List<ISignalNode> GetInputs();
    List<ISignalNode> GetOutputs();

    float GetResistance();                      // Для подсчёта тока
    void ProcessSignal(SignalData input);      // Обработка полученного сигнала
    SignalData GetOutputSignal();              // Сигнал, который выходит из узла
}
