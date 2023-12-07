using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Data")]
    public EnemyData data;

    [Header("Stats")]
    public float currentHP;

    [Header("Settings")]
    public float timeToDestroy;

    [Header("Components")]
    public Transform cannon;
    public GameObject toRemove;

    [Header("Status")]
    public bool active;
    public bool damageable;
    public bool isMoving;

    [Header("Proyectiles")]
    public ObjectPool standardPool;

    [Header("Events")]
    public UnityEvent hit;
    public UnityEvent destroyed;

    protected SpriteRenderer _renderer;
    private Coroutine _movingRoutine;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init()
    {
        currentHP = data.hp;
        active = true;
        damageable = true;

        _renderer = GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// Get damage.
    /// </summary>
    /// <param name="damage">float</param>
    public void GetDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0f)
        {
            if (! data.isBoss)
            {
                Death();
            }
        }
        else
        {
            hit?.Invoke();
        }
    }

    /// <summary>
    /// Base shoot mechanic for enemies.
    /// </summary>
    public void Shoot()
    {
        GameObject proyectile = standardPool.SpawnPrefab(cannon.localPosition);

        if (proyectile)
        {
            EnemyProyectile standard = proyectile.GetComponent<EnemyProyectile>();
            standard.Shoot(standardPool.gameObject.transform);
        }
    }

    /// <summary>
    /// Enemy defeated.
    /// </summary>
    public void Death()
    {
        active = false;
        damageable = false;
        gameObject.tag = "Untagged";
        _renderer.sprite = null;

        if (toRemove != null)
        {
            toRemove.SetActive(false);
        }

        GivePlayerScore();

        destroyed?.Invoke();

        Destroy(gameObject, timeToDestroy);
    }

    /// <summary>
    /// Move enemy to game point in the scene.
    /// </summary>
    /// <param name="destiny">Transform</param>
    /// <param name="speedMult">float</param>
    public void Move(Transform destiny, float speedMult = 1f)
    {
        if (!isMoving && _movingRoutine == null)
        {
            _movingRoutine = StartCoroutine(MovingRoutine(destiny, speedMult));
        }
    }

    /// <summary>
    /// Moving coroutine.
    /// </summary>
    /// <param name="destiny">Transform</param>
    /// <param name="speedMult">float</param>
    /// <returns>IEnumerator</returns>
    public IEnumerator MovingRoutine(Transform destiny, float speedMult = 1f)
    {
        isMoving = true;
        float moveSpeed = speedMult * data.speed;

        while (Vector3.Distance(transform.position, destiny.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destiny.position, moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();

            if (!active)
            {
                yield break;
            }
        }

        transform.position = destiny.position;

        isMoving = false;
        _movingRoutine = null;
    }

    /// <summary>
    /// Update player score when this enemy is
    /// destroyed.
    /// </summary>
    public void GivePlayerScore()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.UpdateScore(data.scoreGiven);
    }

    /// <summary>
    /// Control enemy trigger collisions.
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (damageable && other.CompareTag("Proyectile"))
            {
                PlayerProyectile proyectile = other.GetComponent<PlayerProyectile>();
                GetDamage(proyectile.GetDamage());
                proyectile.Remove();
            }

            if (other.CompareTag("RemoveItems"))
            {
                Destroy(gameObject);
            }
        }
    }

}
