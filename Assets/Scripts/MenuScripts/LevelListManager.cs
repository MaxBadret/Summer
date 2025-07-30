using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LevelListManager : MonoBehaviour
{
    [Header("Названия всех уровней в игре")]
    public List<string> levelNames = new();

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "progress.json");
        EnsureProgressFile();
    }

    // 🔧 Создание или обновление файла
    private void EnsureProgressFile()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            LevelProgressData existingData = JsonUtility.FromJson<LevelProgressData>(json);

            // Добавим новые уровни, если их нет
            foreach (string levelName in levelNames)
            {
                if (!existingData.levels.Exists(l => l.name == levelName))
                {
                    existingData.levels.Add(new LevelEntry(levelName));
                }
            }

            SaveProgress(existingData);
        }
        else
        {
            LevelProgressData newData = new LevelProgressData();
            foreach (string name in levelNames)
            {
                newData.levels.Add(new LevelEntry(name));
            }
            SaveProgress(newData);
        }

        Debug.Log("✔️ Прогресс-файл создан или обновлён: " + saveFilePath);
    }

    private void SaveProgress(LevelProgressData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    // 🔘 Для ручного вызова из инспектора
#if UNITY_EDITOR
    [ContextMenu("🧪 Принудительно создать/обновить прогресс-файл")]
    private void DebugCreateFile()
    {
        EnsureProgressFile();
    }
#endif
}
