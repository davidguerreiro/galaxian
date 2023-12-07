using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    public Player player;
    public LevelManager levelManager;
    public LevelLoop levelLoop;
    public MovementBoard board;
    public Spawners spawners;
    public Backgrounds backgrounds;

    [Header("UIs")]
    public GameplayUI gameplayUI;
    public StartLevelUI startLevelUI;
    public EndLevelUI endLevelUI;
    public PauseUI pauseUI;

    [Header("Events")]
    public UnityEvent onGameOver;

    
    
    // Start is called before the first frame update
    void Start()
    {
        InitDependencies();
        InitUIs();
        StartLevel();
    }

    /// <summary>
    /// Init game modules and dependencies.
    /// </summary>
    public void InitDependencies()
    {
        // Init player.
        player.InitDependencies();

        // Init level manager.
        levelManager.Init(player, startLevelUI, endLevelUI, pauseUI);

        // Init movement board.
        board.InitBoard();

        // Init spawners.
        spawners.InitSpawners();

        // Init backgrounds.
        backgrounds.InitBackgrounds();

        // Init level loop.
        levelLoop.InitLevelLoop();
    }
    
    /// <summary>
    /// Init game UIS.
    /// </summary>
    public void InitUIs()
    {
        gameplayUI.InitDependencies(player);
        startLevelUI.InitDependencies(levelManager.data);
        endLevelUI.InitDependencies(this);
    }

    /// <summary>
    /// Start level once all dependencies are loaded.
    /// </summary>
    public void StartLevel()
    {
        levelManager.InitLevel();
    }

    /// <summary>
    /// Game over logic.
    /// </summary>
    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    /// <summary>
    /// Load next level.
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelManager.data.nextLevelData.levelName);
    }
}
