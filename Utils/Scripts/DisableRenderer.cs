using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        DisableCurrentRender();
    }

    /// <summary>
    /// Disable sprite rendered in screen.
    /// </summary>
    public void DisableCurrentRender()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_renderer)
        {
            _renderer.sprite = null;
        }
    }
}
