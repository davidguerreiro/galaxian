using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arturus : Enemy
{
    [Header("Components")]
    public TripleCannon tripleCannon;
    public EnemyCannon leftCannon;
    public EnemyCannon rightCannon;
    public SawShuttle leftShuttle;
    public SawShuttle rightShuttle;
    public GameObject shield;
    public GameObject defeatedAnim;
    public GameObject propulsion;


    [Header("Settings")]
    public int phase;

    [HideInInspector]
    public Coroutine _battleLoop;

    private bool _ready;
    private Coroutine _spawn;
    private Coroutine _move;
    private Coroutine _shootingCannon;
    private Coroutine _shootingShuttles;
    private Coroutine _phaseLoop;
    private MovementBoard _board;
    private BoxCollider _boxCollider;
    private BoxCollider _tripleCannonCollider;
    private BoxCollider _shuttleLeftCollider;
    private BoxCollider _shuttleRIghtCollider;

    // Update is called once per frame
    void Update()
    {
        if (active && _ready)
        {
            if (_battleLoop == null)
            {
                _battleLoop = StartCoroutine(BattleLoop());
            }

            CheckForDefeated();
        }
    }

    /// <summary>
    /// Spawn enemy logic.
    /// </summary>
    /// <param name="initPoint"></param>
    /// <param name="movementBoard">MovementBoard</param>
    public void Spawn(Transform initPoint, MovementBoard movementBoard)
    {
        if (_spawn == null)
        {
            _spawn = StartCoroutine(SpawnRoutine(initPoint, movementBoard));
        }
    }

    /// <summary>
    /// Set boss ready.
    /// </summary>
    public void SetReady()
    {
        _ready = true;

        _tripleCannonCollider.enabled = true;
        _shuttleLeftCollider.enabled = true;
        _shuttleRIghtCollider.enabled = true;

        shield.SetActive(true);
    }

    /// <summary>
    /// Spawn coroutine.
    /// </summary>
    /// <param name="initPoint">Transform</param>
    /// <param name="movementBoard">MovementBoard</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnRoutine(Transform initPoint, MovementBoard movementBoard)
    {
        Init();
        InitDependencies();

        _board = movementBoard;

        Move(initPoint, 2f);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _spawn = null;
    }

    /// <summary>
    /// Boss movement perform.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ToMove()
    {
        int toMove = Random.Range(2, 12);

        Transform movementPoint = _board.GetMovementPoint(1, toMove);

        Move(movementPoint);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _move = null;
    }

    /// <summary>
    /// Shoot tripple cannon.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ShootTripleCannon()
    {
        tripleCannon.ShootFromEnemy();
        yield return new WaitForSeconds(1f);

        tripleCannon.ShootFromEnemy();
        yield return new WaitForSeconds(1f);

        tripleCannon.ShootFromEnemy();
        yield return new WaitForSeconds(2f);

        _shootingCannon = null;
    }

    /// <summary>
    /// Shooting shuttles.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ShootShuttle()
    {
        bool leftShooted = false;
        bool rightShooted = false;

        if (leftShuttle != null && leftShuttle.active)
        {
            leftShuttle.ShootShuttle();
            leftShooted = true;
        }

        if (rightShuttle != null && rightShuttle.active)
        {
            rightShuttle.ShootShuttle();
            rightShooted = true;
        }

        if (leftShooted)
        {
            while (leftShuttle.shootingRoutine != null)
            {
                if (leftShuttle == null || !leftShuttle.active)
                {
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        if (rightShooted)
        {
            while (rightShuttle.shootingRoutine != null)
            {
                if (rightShuttle == null || !rightShuttle.active)
                {
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        _shootingShuttles = null;
    }

    /// <summary>
    /// Phase one battle loop.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PhaseOneBattleLoop()
    {
        yield return new WaitForSeconds(.5f);

        if (_shootingCannon == null && tripleCannon != null && tripleCannon.enemy.active)
        {
            _shootingCannon = StartCoroutine(ShootTripleCannon());
        }

        if (_shootingShuttles == null)
        {
            _shootingShuttles = StartCoroutine(ShootShuttle());
        }

        while (_shootingCannon != null || _shootingShuttles != null)
        {
            yield return new WaitForFixedUpdate();
        }

        _phaseLoop = null;
    }

    /// <summary>
    /// Phase two battle loop.
    /// </summary>
    /// <returns>IEnumeraror</returns>
    private IEnumerator PhaseTwoBattleLoop()
    {
        float counter = 0;

        shield.SetActive(false);
        _boxCollider.enabled = true;
        yield return new WaitForSeconds(.5f);

        leftCannon.Shoot();
        rightCannon.Shoot();

        yield return new WaitForSeconds(.5f);

        while (counter < 4)
        {
            _move = StartCoroutine(ToMove());
            
            while (_move != null)
            {
                yield return new WaitForFixedUpdate();
            }

            counter++;
        }

        _phaseLoop = null;
    }

    /// <summary>
    /// Battle loop coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator BattleLoop()
    {
        if (phase == 0 && _phaseLoop == null)
        {
            _phaseLoop = StartCoroutine(PhaseOneBattleLoop());
            
            while (_phaseLoop != null)
            {
                yield return new WaitForFixedUpdate();
            }

            if (tripleCannon == null && leftShuttle == null && rightShuttle == null)
            {
                phase++;
            }
        }

        if (phase == 1 && _phaseLoop == null)
        {
            _phaseLoop = StartCoroutine(PhaseTwoBattleLoop());

            while(_phaseLoop != null)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        _battleLoop = null;
    }

    /// <summary>
    /// Check if boss is defeated.
    /// </summary>
    public void CheckForDefeated()
    {
        if (currentHP <= 0f)
        {
            StopCoroutine(_battleLoop);
            _ready = false;

            StartCoroutine(Defeated());
        }
    }

    /// <summary>
    /// Boss defeated logic.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Defeated()
    {
        active = false;
        damageable = false;
        gameObject.tag = "Untagged";
        _renderer.sprite = null;
        _boxCollider.enabled = false;

        propulsion.SetActive(false);

        defeatedAnim.SetActive(true);
        yield return new WaitForSeconds(3.5f);
            
        defeatedAnim.SetActive(false);
        _battleLoop = null;

        destroyed?.Invoke();
        Destroy(this);
    }

    /// <summary>
    /// Init boss dependencies.
    /// </summary>
    private void InitDependencies()
    {
        _boxCollider = GetComponent<BoxCollider>();
        leftShuttle.InitComponents();
        rightShuttle.InitComponents();

        _tripleCannonCollider = tripleCannon.gameObject.GetComponent<BoxCollider>();
        _shuttleLeftCollider = leftShuttle.gameObject.GetComponent<BoxCollider>();
        _shuttleRIghtCollider = rightShuttle.gameObject.GetComponent<BoxCollider>();
    }
}
