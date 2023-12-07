using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOveItem : MonoBehaviour
{
    [Header("Components")]
    public GameObject pointer;
    private AudioComponent _audio;

    /// <summary>
    /// Show navegable pointer.
    /// </summary>
    public void ShowPointer()
    {
        pointer.SetActive(true);

        if (_audio == null)
        {
            _audio = GetComponent<AudioComponent>();
        }

        _audio.PlaySound(0);
    }

    /// <summary>
    /// Hide navegable pointer.
    /// </summary>
    public void HidePointer()
    {
        pointer.SetActive(false);
    }

    /// <summary>
    /// Retry current level.
    /// </summary>
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Back to main menu.
    /// </summary>
    public void RestartGame()
    {
        // TODO: Change this by title screen.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
