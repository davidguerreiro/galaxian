using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;

public class PlayerCombat : MonoBehaviour
{
    [Header("State")]
    public bool canShoot;
    public bool damageable;
    public bool shieldActive;

    [Header("Settings")]
    public float invencibleDuration;
    public float shieldDuration;

    [Header("Components")]
    public Transform cannon;
    public Transform rocketCannonLeft;
    public Transform rocketCannonRight;

    [Header("Standard Proyectiles")]
    public ObjectPool stantardPool;
    public ObjectPool rocketLeftPool;
    public ObjectPool rocketRightPool;

    [Header("Events")]
    public UnityEvent hit;
    public UnityEvent lifeLost;
    public UnityEvent noMoreLifes;
    public UnityEvent enableShield;
    public UnityEvent disableShield;

    [HideInInspector]
    public bool isShooting;

    [HideInInspector]
    public bool isRocketShooting;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public Coroutine getDamage;

    [HideInInspector]
    public PlayerAdditionalWeapons additionalWeapons;

    private Rewired.Player _playerInput;
    private Coroutine _shieldRoutine;

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            CheckStandardShoot();
            CheckRocketShoot();
        }
    }

    /// <summary>
    /// Shoot standard proyectile.
    /// </summary>
    public void CheckStandardShoot()
    {
        if (!isShooting && _playerInput.GetButtonDown("MainShoot"))
        {
            if (additionalWeapons.current != "")
            {
                additionalWeapons.ShootWeapon();
            } else
            {
                StartCoroutine(Shoot());
            }
        }
    }

    /// <summary>
    /// Check for rocket shoot user input.
    /// </summary>
    public void CheckRocketShoot()
    {
        if (!isRocketShooting && player.rockets > 0 && _playerInput.GetButtonDown("AdditionalShoot"))
        {
            StartCoroutine(ShootRockets());
        }
    }

    /// <summary>
    /// Shoot standard proyectile coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Shoot()
    {
        GameObject proyectile = stantardPool.SpawnPrefab(cannon.localPosition);

        if (proyectile)
        {
            PlayerProyectile standard = proyectile.GetComponent<PlayerProyectile>();

            standard.Shoot(stantardPool.gameObject.transform);

            yield return new WaitForSeconds(standard.GetCooling());
        }

        isShooting = false;
    }

    /// <summary>
    /// Rocket shooting.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator ShootRockets()
    {
        isRocketShooting = true;

        GameObject rocketRight = rocketRightPool.SpawnPrefab(rocketCannonRight.localPosition);
        GameObject rocketLeft = rocketLeftPool.SpawnPrefab(rocketCannonLeft.localPosition);

        if (rocketCannonLeft && rocketCannonRight)
        {
            PlayerProyectile rocketLeftProyectile = rocketLeft.GetComponent<PlayerProyectile>();
            PlayerProyectile rocketRightProyectile = rocketRight.GetComponent<PlayerProyectile>();

            rocketLeftProyectile.Shoot(rocketLeftPool.gameObject.transform);
            rocketRightProyectile.Shoot(rocketRightPool.gameObject.transform);

            player.UpdateRockets(-2);

            yield return new WaitForSeconds(rocketRightProyectile.GetCooling());
        }

        isRocketShooting = false;
    }

    /// <summary>
    /// Get damage method.
    /// </summary>
    /// <param name="damage">int</param>
    public void GetDamage(int damage)
    {
        if (getDamage == null)
        {
            getDamage = StartCoroutine(GetDamageRoutine(damage));
        }
    }

    /// <summary>
    /// Get damage coroutine
    /// </summary>
    /// <param name="damage">int</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator GetDamageRoutine(int damage)
    {
        SetInvencible();

        player.UpdateHealth(damage * -1);

        if (player.health <= 0)
        {
            Destroyed();
            getDamage = null;

            yield break;
        }

        hit?.Invoke();

        yield return new WaitForSeconds(invencibleDuration);

        RemoveInvencible();

        getDamage = null;
    }

    /// <summary>
    /// Player ship destroyed.
    /// </summary>
    public void Destroyed()
    {
        player.lifes -= 1;

        player.playerController.StopMovement();
        SetInvencible();
        RestrictShooting();

        lifeLost?.Invoke();

        if (player.lifes > 0)
        {
            StartCoroutine(player.playerSpawn.Respawn(3f));
        } else
        {
            noMoreLifes?.Invoke();
        }
    }

    /// <summary>
    /// Enable shield.
    /// </summary>
    public void EnableShield()
    {  
        if (_shieldRoutine != null)
        {
            StopCoroutine(_shieldRoutine);
            _shieldRoutine = null;
        }

        _shieldRoutine = StartCoroutine(EnableShieldRoutine());
    }

    /// <summary>
    /// Enable player shield.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator EnableShieldRoutine()
    {
        shieldActive = true;
        enableShield?.Invoke();

        yield return new WaitForSeconds(shieldDuration);

        DisableShield();

        _shieldRoutine = null;
    }

    /// <summary>
    /// Disable shield.
    /// </summary>
    /// <param name="disableRoutine">bool</param>
    public void DisableShield(bool disableRoutine = false)
    {
        shieldActive = false;
        disableShield?.Invoke();

        if (disableRoutine)
        {
            StopCoroutine(_shieldRoutine);
            _shieldRoutine = null;
        }
    }

    /// <summary>
    /// Allow player shooting controlling.
    /// </summary>
    public void AllowShooting()
    {
        canShoot = true;
    }

    /// <summary>
    /// Restrict player shooting.
    /// </summary>
    public void RestrictShooting()
    {
        canShoot = false;
    }

    /// <summary>
    /// Avoid all damage.
    /// </summary>
    public void SetInvencible()
    {
        damageable = false;
    }

    /// <summary>
    /// Allow get damage.
    /// </summary>
    public void RemoveInvencible()
    {
        damageable = true;
    }

    /// <summary>
    /// Player combat wrapper to assign weapon.
    /// </summary>
    /// <param name="weaponName">string</param>
    public void AssignWeapon(string weaponName)
    {
        additionalWeapons.AssignWeapon(weaponName);
    }

    /// <summary>
    /// Restore player weapons.
    /// </summary>
    public void RestoreWeapons()
    {
        if (player.rockets == 0)
        {
            player.UpdateRockets(10);
        }

        additionalWeapons.DisableWeapon();
    }

    

    /// <summary>
    /// Trigger player enter collisions.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (damageable && !shieldActive)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyProyectile") || other.CompareTag("Obstacle"))
            {
                int damage = CalculateDamage(other.gameObject);
                GetDamage(damage);
            }
        }
    }

    /// <summary>
    /// Trigger player stay collisions.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (damageable && !shieldActive)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyProyectile") || other.CompareTag("Obstacle"))
            {
                int damage = CalculateDamage(other.gameObject);
                GetDamage(damage);
            }
        }
    }

    /// <summary>
    /// Calculate the damage the player get from enemy collision.
    /// </summary>
    /// <param name="damageableCollided">GameObject</param>
    /// <returns>int</returns>
    private int CalculateDamage(GameObject damageableCollided)
    {
        int damage = 0;

        // damaged by enemy.
        if (damageableCollided.CompareTag("Enemy"))
        {
            Enemy enemy = damageableCollided.GetComponent<Enemy>();
            damage = enemy.data.collisionDamage;
        }

        // damaged by enemy proyectile.
        if (damageableCollided.CompareTag("EnemyProyectile"))
        {
            EnemyProyectile proyectile = damageableCollided.GetComponent<EnemyProyectile>();
            damage = proyectile.GetDamage();
            proyectile.Remove();
        }

        // damaged by obstacle.
        if (damageableCollided.CompareTag("Obstacle"))
        {
            FailingObstacle obstacle = damageableCollided.GetComponent<FailingObstacle>();
            damage = obstacle.GetCollisionDamage();
        }

        return damage;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init(Player player)
    {
        this.player = player;
        additionalWeapons = GetComponent<PlayerAdditionalWeapons>();
        additionalWeapons.DisableWeapon();
        _playerInput = ReInput.players.GetPlayer(0);
    }
}
