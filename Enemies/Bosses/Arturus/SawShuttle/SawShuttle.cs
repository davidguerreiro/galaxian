using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShuttle : Enemy
{
    [Header("Proyectile")]
    public Animator proyectile;

    [Header("Components")]
    public GameObject proyectileWrapper;

    [Header("Settings")]
    public string proyectileType;

    [HideInInspector]
    public Coroutine shootingRoutine;

    private AudioComponent _audio;
        
    /// <summary>
    /// Init class method.
    /// </summary>
    public void InitComponents()
    {
        _audio = GetComponent<AudioComponent>();
    }

    /// <summary>
    /// Shoot mechanic saw proyectile.
    /// </summary>
    public void ShootShuttle()
    {
        if (shootingRoutine == null)
        {
            shootingRoutine = StartCoroutine(ShootCoroutine());
        }
    }

    /// <summary>
    /// Shoot corotine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ShootCoroutine()
    {
        int shootType = Random.Range(0, 2);

        _audio.PlaySound(0);
        proyectile.SetTrigger("Prepare");
        yield return new WaitForSeconds(.5f);

        _audio.PlaySound(0);

        if (shootType == 0)
        {
            proyectile.SetTrigger("ThrowStraight");
            yield return new WaitForSeconds(2f);
        }
        else
        {
            if (proyectileType == "left")
            {
                proyectile.SetTrigger("ThrowLeft");
            }
            else {
                proyectile.SetTrigger("ThrowRight");
            }

            yield return new WaitForSeconds(2.5f);
        }

        shootingRoutine = null;
    }

    /// <summary>
    /// Logic trigged when destroyed.
    /// </summary>
    public void Destroyed()
    {
        proyectileWrapper.SetActive(false);
    }
}
