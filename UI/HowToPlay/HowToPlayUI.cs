using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Rewired;

public class HowToPlayUI : MonoBehaviour
{
    [Header("Components")]
    public FadeElement overlay;
    public GameObject pressStart;

    [Header("Settings")]
    public string sceneToLoad;

    private Rewired.Player _playerInput;
    private AudioComponent _audio;
    private bool _readyToReadInput;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(DisplayControlList());
    }

    private void Update()
    {
        if (_readyToReadInput)
        {
            ListenForUserInput();
        }
    }

    /// <summary>
    /// Display control list.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayControlList()
    {
        yield return new WaitForSeconds(.5f);

        overlay.FadeOut();
        yield return new WaitForSeconds(.5f);

        pressStart.SetActive(true);

        _readyToReadInput = true;
    }

    /// <summary>
    /// Load next scene.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator LoadNextSecene()
    {
        _readyToReadInput = false;

        pressStart.SetActive(false);
        _audio.PlaySound(0);
        overlay.FadeIn();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// Listen for user input.
    /// </summary>
    private void ListenForUserInput()
    {
        if (_playerInput.GetButtonDown("AfirmativeAction"))
        {
            StartCoroutine(LoadNextSecene());
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init()
    {
        _audio = GetComponent<AudioComponent>();
        _playerInput = ReInput.players.GetPlayer(0);
    }
}
