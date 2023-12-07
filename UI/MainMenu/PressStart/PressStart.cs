using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStart : MonoBehaviour
{
    public GameObject startText;

    private bool _displayed = true;

    void Awake()
    {
        InvokeRepeating("Blink", 0f, .5f);
    }

    /// <summary>
    /// Blink text
    /// </summary>
    public void Blink()
    {
        startText.SetActive(!_displayed);
        _displayed = !_displayed;
    }
}
