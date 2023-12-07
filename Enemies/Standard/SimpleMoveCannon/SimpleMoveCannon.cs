using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveCannon : Enemy
{
    [Header("SimpleCannon Settings")]
    public Vector2 sideMovementPointsRange;
    public int shoots;
    public float shootingCadence;
    public float shootingCooling;

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
    /// Simple move cannon battle loop.
    /// </summary>
    private void BattleLoop()
    {
        if (_shoot == null)
        {
            _shoot = StartCoroutine(Shooting());
        }

        if (_sideWalking == null)
        {
            _sideWalking = StartCoroutine(SideWalking());
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

        Move(initPoint, 5f);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _ready = true;
        _spawn = null;
    }

    /// <summary>
    /// Side walking movement logic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SideWalking()
    {
        int min = (int)sideMovementPointsRange.x;
        int max = (int)sideMovementPointsRange.y + 1;
        int column = Random.Range(min, max);

        Transform movementPoint = _board.GetMovementPoint(_currentRow, column);

        Move(movementPoint);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(.5f);

        _sideWalking = null;
    }

    /// <summary>
    /// Sidewalker shoot logic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator Shooting()
    {
        if (active)
        {
            for (int i = 0; i < shoots; i++)
            {
                Shoot();
                yield return new WaitForSeconds(shootingCadence);
            }
        }

        yield return new WaitForSeconds(shootingCooling);

        _shoot = null;
    }
}
