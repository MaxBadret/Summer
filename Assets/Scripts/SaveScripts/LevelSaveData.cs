using System;
using System.Collections.Generic;

[System.Serializable]
public class LevelStatus
{
    public string name;
    public int completed; // 0 или 1

    public LevelStatus(string name)
    {
        this.name = name;
        this.completed = 0;
    }
}

[System.Serializable]
public class LevelProgressData
{
    public List<LevelStatus> levels = new List<LevelStatus>();
}
