using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRocks : Enemy
{
    [Header("Throwing Rocks Settings")]
    public Rock[] rocks;
    public GameObject rocksWrapper;
    public float rocksAttackDuration;

    private bool _ready;
    private Coroutine _spawn;
    private MovementBoard _board;
    private int[] _rows;
    private int[] _cols;
    private Coroutine _movingToPoints;
    private Coroutine _rocksAttack;
    private Coroutine _battleLoop;
    private Transform _initPoint;

    // Update is called once per frame
    void Update()
    {
        if (active && _ready)
        {
            if (_battleLoop == null)
            {
                _battleLoop = StartCoroutine(BattleLoop());
            }
        }
    }

    /// <summary>
    /// Spawn enemy logic.
    /// </summary>
    /// <param name="initPoint"></param>
    /// <param name="movementBoard">MovementBoard</param>
    /// <param name="rows">int[]</param>
    /// <param name="cols">int[]</param>
    public void Spawn(Transform initPoint, MovementBoard movementBoard, int[] rows, int[] cols)
    {
        if (_spawn == null)
        {
            _spawn = StartCoroutine(SpawnRoutine(initPoint, movementBoard, rows, cols));
        }
    }

    /// <summary>
    /// Spawn coroutine.
    /// </summary>
    /// <param name="initPoint">Transform</param>
    /// <param name="movementBoard">MovementBoard</param>
    /// <param name="rows">int[]</param>
    /// <param name="cols">int[]</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnRoutine(Transform initPoint, MovementBoard movementBoard, int[] rows, int[] cols)
    {
        Init();

        _board = movementBoard;
        _initPoint = initPoint;
        _rows = rows;
        _cols = cols;

        Move(initPoint, 10f);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _ready = true;
        _spawn = null;
    }

    /// <summary>
    /// Enemy battle loop.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator BattleLoop()
    {
        if (_movingToPoints == null)
        {
            _movingToPoints = StartCoroutine(MoveToPositions());
        }

        while (_movingToPoints != null)
        {
            yield return new WaitForFixedUpdate();
        }

        
        if (HasRocks() && _rocksAttack == null)
        {
            _rocksAttack = StartCoroutine(RocksAttack());
        }

        while (_rocksAttack != null)
        {
            yield return new WaitForFixedUpdate();
        }

        _battleLoop = null;
    }

    /// <summary>
    /// Move to different positions accros the game
    /// scene.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator MoveToPositions()
    {
        for (int i = 0; i < _rows.Length; i++)
        {
            Transform movementPoint = _board.GetMovementPoint(_rows[i], _cols[i]);

            Move(movementPoint);

            while (isMoving)
            {
                yield return new WaitForFixedUpdate();
            }

            _movingToPoints = null;
        }
    }

    /// <summary>
    /// Move to init point.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RestoreToInitPoint()
    {
        Move(_initPoint);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _movingToPoints = null;
    }

    /// <summary>
    /// Rocks attack.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    private IEnumerator RocksAttack()
    {
        foreach (Rock rock in rocks)
        {
            if (rock != null)
            {
                StartCoroutine(rock.MoveInOut());
            }
        }

        yield return new WaitForSeconds(rocksAttackDuration);
        _rocksAttack = null;
    }

    /// <summary>
    /// Checks if has rocks.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private bool HasRocks()
    {
        return rocksWrapper.transform.childCount > 0;
    }

    /// <summary>
    /// Remove all rocks if main enemy
    /// destroyed.
    /// </summary>
    public void RemoveAllRocks()
    {
        if (HasRocks())
        {
            foreach (Rock rock in rocks)
            {
                if (rock != null)
                {
                    rock.Death();
                }
            }
        }
    }

}
