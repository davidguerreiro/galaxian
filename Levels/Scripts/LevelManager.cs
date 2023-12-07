using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelManager : MonoBehaviour
{
    public LevelData data;

    [Header("Level Status")]
    public bool paused;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public StartLevelUI startLevelUI;

    [HideInInspector]
    public EndLevelUI endLevelUI;

    [HideInInspector]
    public PauseUI pauseUI;

    private AudioComponent _audioComponent;
    private Rewired.Player _playerInput;

    private void Update()
    {
        if (player.playerController.canMove)
        {
            CheckForPlayerInput();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="startLevelUI">StartLevelUI</param>
    /// <param name="endLevelUI">EndLevelUI</param>
    /// <param name="pauseUI">PauseUI</param>
    public void Init(Player player, StartLevelUI startLevelUI = null, EndLevelUI endLevelUI = null, PauseUI pauseUI = null)
    {
        this.player = player;
        this.startLevelUI = startLevelUI;
        this.endLevelUI = endLevelUI;
        this.pauseUI = pauseUI;
        _audioComponent = GetComponent<AudioComponent>();
        _playerInput = ReInput.players.GetPlayer(0);
    }

    /// <summary>
    /// Init level.
    /// </summary>
    public void InitLevel()
    {
        StartCoroutine(InitLevelRoutine());
    }

    /// <summary>
    /// Main init level coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator InitLevelRoutine()
    {
        PlayeLevelMusic();
        
        // display UI anim.
        if (startLevelUI != null)
        {
            startLevelUI.DisplayUI();
        }

        while (startLevelUI.displayingUI != null)
        {
            yield return new WaitForFixedUpdate();
        }

        // spawn player in level.
        StartCoroutine(player.playerSpawn.Respawn(.5f));
        yield return new WaitForSeconds(.5f);

        startLevelUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// End level.
    /// </summary>
    public void EndLevel()
    {
        StartCoroutine(EndLevelRoutine());
    }

    /// <summary>
    /// Display end level routine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator EndLevelRoutine()
    {
        yield return new WaitForSeconds(.5f);

        _audioComponent.audio.loop = false;
        PlayeLevelMusic(2);
        player.playerAnimations.DisplayEndLevelAnim();
        yield return new WaitForSeconds(1f);

        endLevelUI.DisplayEndGameScreen();
        
    }

    /// <summary>
    /// Play level music.
    /// </summary>
    /// <param name="track">int</param>
    public void PlayeLevelMusic(int track = 0)
    {
        _audioComponent.PlaySound(track);
    }

    /// <summary>
    /// Stop level music.
    /// </summary>
    public void StopLevelMusic()
    {
        _audioComponent.StopAudio();
    }

    /// <summary>
    /// Pause level music.
    /// </summary>
    public void PauseLevelMusic()
    {
        _audioComponent.PauseAudio();
    }

    /// <summary>
    /// Resume level music.
    /// </summary>
    public void ResumeLevelMusic()
    {
        _audioComponent.ResumeAudio();
    }

    /// <summary>
    /// Pause game during gameplay.
    /// </summary>
    public void PauseGame()
    {
        pauseUI.DisplayUI();
        player.playerCombat.RestrictShooting();
        Time.timeScale = 0;
        paused = true;
    }

    /// <summary>
    /// Resume game after pause.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.HideUI();
        player.playerCombat.AllowShooting();
        paused = false;
    }

    /// <summary>
    /// Exit game.
    /// </summary>
    public void ExitApp()
    {
        Application.Quit();
    }

    /// <summary>
    /// Check for player input related level actions.
    /// </summary>
    private void CheckForPlayerInput()
    {
        if (_playerInput.GetButtonDown("AfirmativeAction"))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (_playerInput.GetButtonDown("ExitApp"))
        {
            ExitApp();
        }
    }

    /// <summary>
    /// Fade level music.
    /// </summary>
    public void FadeLevelMusic()
    {
        StartCoroutine(_audioComponent.FadeOutSongRoutine(2f));
    }
}
