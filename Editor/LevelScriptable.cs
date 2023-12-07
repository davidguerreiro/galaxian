using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelScriptable : MonoBehaviour
{
    [MenuItem("Assets/Create/LevelData")]
    /// <summary>
    /// Add level data scriptable item to
    /// create menu.
    /// </summary>
    public static void AddLevelDataScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<LevelData>();

        // if needs preconfiguration, add here.
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path += "/NewLevelData.asset";

        ProjectWindowUtil.CreateAsset(asset, path);
    }
}
