using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyScriptable : MonoBehaviour
{
    [MenuItem("Assets/Create/Enemy/EnemyData")]
    /// <summary>
    /// Add Enemy data ¬scriptable item to
    /// player menu create.
    /// </summary>
    public static void AddActorDataScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<EnemyData>();

        // if needs preconfiguration, add here.
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path += "/NewEnemyData.asset";

        ProjectWindowUtil.CreateAsset(asset, path);
    }

    [MenuItem("Assets/Create/Enemy/Proyectile/Standard")]
    /// <summary>
    /// Add player standard proyectile scriptable item to
    /// player menu create.
    /// </summary>
    public static void AddEnemyProyectileScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<EnemyProyectileData>();

        // if needs preconfiguration, add here.
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path += "/NewEnemyProyectile.asset";

        ProjectWindowUtil.CreateAsset(asset, path);
    }

    [MenuItem("Assets/Create/Enemy/Obstacle")]
    /// <summary>
    /// Add obstacle scriptable item to
    /// enemy menu create.
    /// </summary>
    public static void AddObstacleScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<Obstacle>();

        // if needs preconfiguration, add here.
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path += "/NewObstacle.asset";

        ProjectWindowUtil.CreateAsset(asset, path);
    }
}
