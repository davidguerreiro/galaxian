using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelEvents : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject enemies;
    public GameObject bossStart;

    protected Coroutine _levelLoop;
    protected Coroutine _bossBattle;

    protected Spawners spawners;
    protected MovementBoard board;

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init()
    {
        spawners = gameManager.spawners;
        board = gameManager.board;
    }
}
