using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScriptables : MonoBehaviour
{
    [MenuItem("Assets/Create/Player/Proyectile/Standard")]
    /// <summary>
    /// Add player standard proyectile scriptable item to
    /// player menu create.
    /// </summary>
    public static void AddActorDataScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<ProyectileData>();

        // if needs preconfiguration, add here.
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path += "/NewProyectile.asset";

        ProjectWindowUtil.CreateAsset(asset, path);
    }
}
