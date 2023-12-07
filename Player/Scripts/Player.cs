using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int lifes;
    public int health;
    public float speed;
    public int rockets;
    public int score;
    public int maxLifes = 99;
    public int maxHealth = 5;

    [Header("Events")]
    public UnityEvent updateHealth;

    [HideInInspector]
    public PlayerController playerController;

    [HideInInspector]
    public PlayerCombat playerCombat;

    [HideInInspector]
    public PlayerAnimations playerAnimations;

    [HideInInspector]
    public PlayerSpawn playerSpawn;

    /// <summary>
    /// Init player dependencies.
    /// </summary>
    public void InitDependencies()
    {
        // player controller.
        playerController = GetComponent<PlayerController>();
        playerController.Init(speed);

        // player combat.
        playerCombat = GetComponent<PlayerCombat>();
        playerCombat.Init(this);

        // player animations.
        playerAnimations = GetComponent<PlayerAnimations>();
        playerAnimations.Init(playerController, playerCombat);

        // player spawn
        playerSpawn = GetComponent<PlayerSpawn>();
        playerSpawn.Init(this);
    }

    /// <summary>
    /// Restore player health.
    /// </summary>
    public void RestoreHealth()
    {
        health = maxHealth;
        updateHealth?.Invoke();
    }

    /// <summary>
    /// Update player health.
    /// </summary>
    /// <param name="newHealth">int</param>
    public void UpdateHealth(int newHealth)
    {
        health += newHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health < 0)
        {
            health = 0;
        }

        updateHealth?.Invoke();
    }

    /// <summary>
    /// Update lifes.
    /// </summary>
    /// <param name="lifes">int</param>
    public void GetLife(int lifes = 1)
    {
        this.lifes += lifes;

        if (this.lifes > maxLifes)
        {
            this.lifes = maxLifes;
        }

        if (this.lifes < 0)
        {
            this.lifes = 0;
        }
    }

    /// <summary>
    /// Update score.
    /// </summary>
    /// <param name="score">int</param>
    public void UpdateScore(int score)
    {
        this.score += score;
    }

    /// <summary>
    /// Update rockets.
    /// </summary>
    /// <param name="newRockets">int</param>
    public void UpdateRockets(int newRockets)
    {
        rockets += newRockets;
        
        if (rockets > 99)
        {
            rockets = 99;
        }

        if (rockets < 0)
        {
            rockets = 0;
        }
    }
}
