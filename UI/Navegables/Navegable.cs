using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navegable : MonoBehaviour {

    [Header("Navegables")]
    public NavegableItem[] navegables;

    [HideInInspector]
    public static Coroutine navegableRoutine;

    // Start is called before the first frame update
    void Start()
    {
        InitNavegables();   
    }

    /// <summary>
    /// Init navegables.
    /// </summary>
    public void InitNavegables()
    {
        foreach (NavegableItem navegable in navegables)
        {
            navegable.Init();
        }
    }

    /// <summary>
    ///  Set navegable routine to null.
    /// </summary>
    public static void SetNavigationRoutineToNull()
    {
        navegableRoutine = null;
    }
}
