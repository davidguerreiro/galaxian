using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class EndLevelUI : MonoBehaviour
{
    [Header("Components")]
    public FadeElement background;
    public GameObject title;
    public GameObject lifesDataRow;
    public GameObject scoreDataRow;
    public GameObject bonus;
    public GameObject pressEnter;
    public TextComponent lifesValue;
    public TextComponent scoreValue;

    private bool _readyToContinue;
    private GameManager _gameManager;
    private Rewired.Player _playerInput;
    private Coroutine _pressEnterAnim;
    private TextComponent _titleTextComponent;

    private void Update()
    {
        if (_readyToContinue)
        {
            ListenForUserInput();

            if (_pressEnterAnim == null)
            {
                _pressEnterAnim = StartCoroutine(PressEnterAnim());
            }

        }
    }

    /// <summary>
    /// Listen for user input to confirm next level.
    /// </summary>
    public void ListenForUserInput()
    {
        if (_playerInput.GetButtonDown("AfirmativeAction")) {
            _gameManager.LoadNextLevel();
        }
    }

    /// <summary>
    /// Wrapper for end UI routine.
    /// </summary>
    public void DisplayEndGameScreen()
    {
        WriteData();
        StartCoroutine(DisplayEndGameScreenRoutine());
    }

    /// <summary>
    /// Display all UI elements in end level screen.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayEndGameScreenRoutine()
    {
        background.FadeIn();
        yield return new WaitForSeconds(2f);

        title.SetActive(true);
        yield return new WaitForSeconds(2f);

        lifesDataRow.SetActive(true);
        yield return new WaitForSeconds(.5f);
        scoreDataRow.SetActive(true);
        yield return new WaitForSeconds(1f);

        // TODO: Call bonus if conditions meet.
        bonus.SetActive(true);

        pressEnter.SetActive(true);

        _readyToContinue = true;
    }

    /// <summary>
    /// Press enter code animation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PressEnterAnim()
    {
        yield return new WaitForSeconds(1f);
        pressEnter.SetActive(false);

        yield return new WaitForSeconds(1f);
        pressEnter.SetActive(true);

        _pressEnterAnim = null;
    }

    /// <summary>
    /// Write data on screen.
    /// </summary>
    public void WriteData()
    {
        LevelData data = _gameManager.levelManager.data;

        _titleTextComponent.UpdateContent("Level  " + data.levelNumber.ToString() + "  completed!");

        lifesValue.UpdateContent(_gameManager.player.lifes.ToString());
        scoreValue.UpdateContent(_gameManager.player.score.ToString());
    }

    /// <summary>
    /// Init UI dependencies.
    /// </summary>
    /// <param name="levelManager">LevelManager</param>
    public void InitDependencies(GameManager gameManager)
    {
        _gameManager = gameManager;
        _playerInput = ReInput.players.GetPlayer(0);
        _titleTextComponent = title.GetComponent<TextComponent>();
    }
}
