using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Rows")]
    public GameObject leftColumn;
    public GameObject rightColumn;
    public GameObject topColumn;

    private Transform[] left = new Transform[8];
    private Transform[] right = new Transform[8];
    private Transform[] top = new Transform[15];

    /// <summary>
    /// Init spawners.
    /// </summary>
    public void InitSpawners()
    {
        int i = 0;
        int j = 0;
        int k = 0;

        foreach (Transform transform in leftColumn.transform)
        {
            left[i] = transform;
            i++;
        }

        foreach (Transform transform in rightColumn.transform)
        {
            right[j] = transform;
            j++;
        }

        foreach (Transform transform in topColumn.transform)
        {
            top[k] = transform;
            k++;
        }
    }

    /// <summary>
    /// Get spawner.
    /// </summary>
    /// <param name="column">string</param>
    /// <param name="index">int</param>
    /// <returns>Transform</returns>
    public Transform GetSpawner(string column, int index)
    {
        Transform spawner;

        switch (column)
        {
            case "left":
                spawner = left[index];
                break;
            case "right":
                spawner = right[index];
                break;
            case "top":
                spawner = top[index];
                break;
            default:
                spawner = null;
                break;
        }

        return spawner;
    }
}
