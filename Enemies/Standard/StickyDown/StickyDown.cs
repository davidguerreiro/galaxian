using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyDown : Enemy
{
    [Header("StickyDown Settings")]
    public float delayFrontalAttack;

    private bool _ready;
    private bool _readyForAttack;
    private Coroutine _spawn;
    private Coroutine _prepareAttack;

    // Update is called once per frame
    void Update()
    {
        if (active && _ready)
        {
            BattleLoop();
        }
    }

    /// <summary>
    /// Sticky down battle loop.
    /// </summary>
    private void BattleLoop()
    {
        if (_prepareAttack == null)
        {
            _prepareAttack = StartCoroutine(PrepareForAttack());
        }

        if (_readyForAttack)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// Spawn enemy.
    /// </summary>
    /// <param name="initPoint"></param>
    public void Spawn(Transform initPoint)
    {
        if (_spawn == null)
        {
            _spawn = StartCoroutine(SpawnRoutine(initPoint));
        }
    }

    /// <summary>
    /// Spawn enemy coroutine.
    /// </summary>
    /// <param name="initPoint">Transform</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnRoutine(Transform initPoint)
    {
        Init();

        Move(initPoint, 10f);

        while (isMoving)
        {
            yield return new WaitForFixedUpdate();
        }

        _ready = true;
        _spawn = null;
    }

    /// <summary>
    /// Prepare to perform drop down attack.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PrepareForAttack()
    {
        yield return new WaitForSeconds(delayFrontalAttack);
        _readyForAttack = true;

        gameObject.GetComponent<EnemySoundAnims>().PlayBattleSound(1);
    }

    /// <summary>
    /// Move down at high speed.
    /// </summary>
    private void MoveDown()
    {
        Vector2 current = transform.position;
        transform.position = new Vector2(current.x, current.y -= data.speed * Time.deltaTime);
    }
}
