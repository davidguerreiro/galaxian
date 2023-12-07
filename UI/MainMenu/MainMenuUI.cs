using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class MainMenuUI : MonoBehaviour
{
    [Header("Components")]
    public FadeElement overlay;
    public GameObject gameTitle;
    public GameObject logo;
    public GameObject copyText;
    public GameObject pressStart;
    public GameObject mainMenu;

    [Header("Settings")]
    public string sceneNameToLoadArcade;

    private AudioComponent _audio;
    private Coroutine _logoRoutine;
    private bool _readyForMenu;
    private Rewired.Player _playerInput;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(DisplayTitleRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_readyForMenu)
        {
            ListenForUserInput();
        }
    }

    public void ListenForUserInput()
    {
        if (_playerInput.GetButtonDown("AfirmativeAction"))
        {
            DisplayMainMenu();
        }
    }

    /// <summary>
    /// Display title routine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator DisplayTitleRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        overlay.FadeOut();

        yield return new WaitForSeconds(.1f);
        gameTitle.SetActive(true);

        yield return new WaitForSeconds(2f);

        _audio.PlaySound(0);
        yield return new WaitForSeconds(1f);

        pressStart.SetActive(true);
        logo.SetActive(true);
        copyText.SetActive(true);

        _audio.SetAudioLoop();
        _audio.PlaySound(1);

        _readyForMenu = true;
    }

    /// <summary>
    /// Display main menu.
    /// </summary>
    public void DisplayMainMenu()
    {
        mainMenu.SetActive(true);
        pressStart.SetActive(false);
        _readyForMenu = false;
    }

    /// <summary>
    /// Hide main menu.
    /// </summary>
    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Init arcade coroutine.
    /// </summary>
    public void InitArcade()
    {
        StartCoroutine(InitArcadeRoutine());
    }

    /// <summary>
    /// Init arcade coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator InitArcadeRoutine()
    {
        HideMainMenu();

        overlay.FadeIn();
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(sceneNameToLoadArcade);
    }

    /// <summary>
    /// Exit game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init()
    {
        _audio = GetComponent<AudioComponent>();
        _playerInput = ReInput.players.GetPlayer(0);
    }
}
