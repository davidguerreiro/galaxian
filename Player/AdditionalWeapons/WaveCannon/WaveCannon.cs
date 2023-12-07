using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveCannon : MonoBehaviour
{
    [Header("Components")]
    public Transform cannon;
    public ObjectPool pool;

    [Header("Events")]
    public UnityEvent atShoot;

    /// <summary>
    /// Shoot wave.
    /// </summary>
    public void Shoot()
    {
        GameObject proyectile = pool.SpawnPrefab(cannon.localPosition);

        if (proyectile)
        {
            PlayerProyectile wave = proyectile.GetComponent<PlayerProyectile>();
            wave.Shoot(pool.gameObject.transform);

            atShoot?.Invoke();
        }
    }
}
