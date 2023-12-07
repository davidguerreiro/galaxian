using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteWalker : Enemy
{
    [Header("WhiteWalker Settings")]
    public Vector2 sideMovementPointsRange;
    public Vector2 shootingCooolingRange;

    private bool _ready;
    private int _currentRow;
    private Coroutine _spawn;
    private Coroutine _sideWalking;
    private Coroutine _shoot;
    private MovementBoard _board;

    // Update is called once per frame
    void Update()
    {
        if (active && _ready)
        {
            BattleLoop();
        }
    }

    /// <summary>
    /// Spawn enemy logic.
    /// </summary>
    /// <param name="initPoint"></param>
    /// <param name="movementBoard">MovementBoard</param>
    /// <param name="currentRow">int</param>
    public void Spawn(Transform initPoint, MovementBoard movementBoard, int currentRow)
    {
        if (_spawn == null)
        {
            _spawn = StartCoroutine(SpawnRoutine(initPoint, movementBoard, currentRow));
        }
    }

    /// <summary>
    /// Spawn coroutine.
    /// </summary>
    /// <param name="initPoint">Transform</param>
    /// <param name="movementBoard">MovementBoard</param>
    /// <param name="currentRow">int</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnRoutine(Transform initPoint, MovementBoard movementBoard, int currentRow)
    {
        Init();

        _currentRow = currentRow;
        _board = movementBoard;

        Move(initPoint, 10f);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _ready = true;
        _spawn = null;
    }

    /// <summary>
    /// Sidewalker battle loop.
    /// </summary>
    private void BattleLoop()
    {
        if (_sideWalking == null)
        {
            _sideWalking = StartCoroutine(SideWalking());
        }

        if (_shoot == null)
        {
            _shoot = StartCoroutine(Shooting());
        }
    }

    /// <summary>
    /// Side walking movement logic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SideWalking()
    {
        float toInitWait = Random.Range(0f, 2f);
        yield return new WaitForSeconds(toInitWait);

        int min = (int)sideMovementPointsRange.x;
        int max = (int)sideMovementPointsRange.y + 1;
        int column = Random.Range(min, max);

        Transform movementPoint = _board.GetMovementPoint(_currentRow, column);

        Move(movementPoint);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        float toWait = Random.Range(1f, 4f);
        yield return new WaitForSeconds(toWait);

        _sideWalking = null;
    }

    /// <summary>
    /// Sidewalker shoot logic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator Shooting()
    {
        float toWait = Random.Range(shootingCooolingRange.x, shootingCooolingRange.y);
        yield return new WaitForSeconds(toWait);

        if (active)
        {
            for (int i = 0; i < 2; i++)
            {
                Shoot();
                yield return new WaitForSeconds(.5f);
            }
        }

        _shoot = null;
    }
}
