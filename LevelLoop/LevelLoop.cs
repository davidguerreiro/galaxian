using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoop : MonoBehaviour
{
    [Header("Loop Events")]
    public UnityEvent levelLoop;

    /// <summary>
    /// Init level loop.
    /// </summary>
    public void InitLevelLoop()
    {
        levelLoop?.Invoke();
    }
}
