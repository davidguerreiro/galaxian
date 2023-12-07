using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public int id;
    public int levelNumber;
    public string levelName;            // must match scene name.
    public string displayName;
    public LevelData nextLevelData;

    [Serializable]
    public enum LevelType
    {
        action,
        bonus
    };

    public LevelType levelType;
}
