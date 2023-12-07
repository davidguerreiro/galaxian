using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("Components")]
    public GameObject wrapper;
    public Image background;
    public Sprite[] backgrounds;

    [HideInInspector]
    public bool displayed;

    private Coroutine _backgroundAnimations;

    // Update is called once per frame
    void Update()
    {
        if (displayed && _backgroundAnimations == null)
        {
            _backgroundAnimations = StartCoroutine(BackgroundAnims());
        }
    }

    /// <summary>
    /// Display pause UI.
    /// </summary>
    public void DisplayUI()
    {
        wrapper.SetActive(true);
        displayed = true;
    }

    /// <summary>
    /// Hide pause UI.
    /// </summary>
    public void HideUI()
    {
        wrapper.SetActive(false);
        displayed = false;
    }

    /// <summary>
    /// Display backgrounds anumation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator BackgroundAnims()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            background.sprite = backgrounds[i];
            yield return new WaitForSecondsRealtime(.5f);
        }

        _backgroundAnimations = null;
    }
}
