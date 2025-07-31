using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LevelProgressManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "level_progress.json");
    private static LevelProgressData currentData;

    // Создаёт файл или подстраивает под список уровней
    public static void CreateOrUpdateProgressFile(List<string> levelNames)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            currentData = JsonUtility.FromJson<LevelProgressData>(json);

            bool changed = false;

            // Если в файле больше уровней, чем в списке — обрезаем
            if (currentData.levels.Count > levelNames.Count)
            {
                currentData.levels.RemoveRange(levelNames.Count, currentData.levels.Count - levelNames.Count);
                changed = true;
            }

            for (int i = 0; i < levelNames.Count; i++)
            {
                if (i >= currentData.levels.Count)
                {
                    currentData.levels.Add(new LevelStatus(levelNames[i]));
                    changed = true;
                }
                else if (currentData.levels[i].name != levelNames[i])
                {
                    currentData.levels[i].name = levelNames[i];
                    changed = true;
                }
            }

            if (changed)
            {
                SaveProgress();
                Debug.Log("🔄 Файл прогресса обновлён.");
            }
            else
            {
                Debug.Log("✅ Файл прогресса актуален.");
            }
        }
        else
        {
            currentData = new LevelProgressData();

            foreach (string name in levelNames)
            {
                currentData.levels.Add(new LevelStatus(name));
            }

            SaveProgress();
            Debug.Log("📄 Файл прогресса создан.");
        }
    }

    // Сохраняет текущие данные в файл
    public static void SaveProgress()
    {
        if (currentData == null) return;

        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(filePath, json);
    }

    // Загружает прогресс из файла (без создания/редактирования)
    public static List<LevelStatus> LoadProgress()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Файл прогресса не найден.");
            return null;
        }

        string json = File.ReadAllText(filePath);
        currentData = JsonUtility.FromJson<LevelProgressData>(json);

        return currentData.levels;
    }

    // Возвращает количество пройденных уровней (completed == 1)
    public static int GetCompletedLevelCount()
    {
        if (currentData == null || currentData.levels == null) return 0;

        int count = 0;
        foreach (var level in currentData.levels)
        {
            if (level.completed == 1) count++;
        }
        return count;
    }

    // Можно добавить метод для установки статуса уровня
    public static void SetLevelCompleted(string levelName, bool completed)
    {
        if (currentData == null || currentData.levels == null) return;

        var level = currentData.levels.Find(l => l.name == levelName);
        if (level != null)
        {
            level.completed = completed ? 1 : 0;
            SaveProgress();
        }
    }
}

