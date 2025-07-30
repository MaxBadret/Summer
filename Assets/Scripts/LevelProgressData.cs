using System;
using System.Collections.Generic;

[Serializable]
public class LevelProgressData
{
    public List<LevelEntry> levels = new();
}

[Serializable]
public class LevelEntry
{
    public string name;
    public bool completed;

    public LevelEntry(string name, bool completed = false)
    {
        this.name = name;
        this.completed = completed;
    }
}
