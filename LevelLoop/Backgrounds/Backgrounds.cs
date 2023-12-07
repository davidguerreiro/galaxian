using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    [Header("Components")]
    public Background[] backgrounds;

    [Header("Settings")]
    public float speed;

    /// <summary>
    /// Init backgrounds.
    /// </summary>
    public void InitBackgrounds()
    {
        // calculate reset position based in last background local position.
        Vector3 originalPosition = backgrounds[backgrounds.Length - 1].gameObject.transform.localPosition;

        foreach (Background background in backgrounds)
        {
            background.Init(speed, originalPosition);
        }
    }

    /// <summary>
    /// Stop all backgrounds
    /// </summary>
    public void StopBackgrounds()
    {
        foreach (Background background in backgrounds)
        {
            background.StopMovement();
        }
    }
}
