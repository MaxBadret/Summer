using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Имя этого уровня (должно совпадать с именем в JSON)")]
    public string levelName;

    [Header("Требуемое выходное напряжение")]
    public float requiredVoltage = 5f;

    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "progress.json");
    }

    private void Update()
    {
        if (CheckWinCondition())
        {
            MarkLevelCompleted();
            enabled = false; // Выключаем LevelManager после выполнения
        }
    }

    private bool CheckWinCondition()
    {
        // 🔌 Проверка выхода схемы на нужное напряжение
        var last = CircuitManager.Instance.GetLastComponent();
        if (last == null) return false;

        SignalData signal = last.GetOutputSignal();
        return Mathf.Abs(signal.Voltage - requiredVoltage) < 0.01f;
    }

    private void MarkLevelCompleted()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("❌ Файл прогресса не найден.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        LevelProgressData data = JsonUtility.FromJson<LevelProgressData>(json);

        bool found = false;
        foreach (var entry in data.levels)
        {
            if (entry.name == levelName)
            {
                if (entry.completed == 0) // Если ещё не пройден
                {
                    entry.completed = 1; // Помечаем как пройденный
                    Debug.Log($"✅ Уровень \"{levelName}\" пройден!");
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.LogWarning($"⚠️ Уровень \"{levelName}\" не найден в json.");
            return;
        }

        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, updatedJson);
    }
}
