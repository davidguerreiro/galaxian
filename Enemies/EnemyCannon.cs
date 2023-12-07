using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    public ObjectPool pool;

    /// <summary>
    /// Shoot enemy cannon.
    /// </summary>
    public void Shoot()
    {
        GameObject proyectile = pool.SpawnPrefab(this.gameObject.transform.localPosition);

        if (proyectile)
        {
            EnemyProyectile standard = proyectile.GetComponent<EnemyProyectile>();
            standard.Shoot(pool.gameObject.transform);
        }
    }
}
