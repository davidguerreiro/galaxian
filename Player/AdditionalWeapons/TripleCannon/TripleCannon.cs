using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TripleCannon : MonoBehaviour
{
    [Header("Components")]
    public Transform centralCannon;
    public Transform rightCannon;
    public Transform leftCannon;
    public ObjectPool centralPool;
    public ObjectPool rightPool;
    public ObjectPool leftPool;

    [Header("Optional Dependencies")]
    public Enemy enemy;

    [Header("Events")]
    public UnityEvent atShoot;

    /// <summary>
    /// Shoot triple shoot cannon.
    /// </summary>
    public void Shoot()
    {
        GameObject centralProyectile = centralPool.SpawnPrefab(centralCannon.localPosition);
        GameObject rightProyectile = rightPool.SpawnPrefab(rightCannon.localPosition);
        GameObject leftProyectile = leftPool.SpawnPrefab(leftCannon.localPosition);
        
        if (centralProyectile && rightProyectile && leftProyectile)
        {
            PlayerProyectile standardCentral = centralProyectile.GetComponent<PlayerProyectile>();
            PlayerProyectile standardRight = rightProyectile.GetComponent<PlayerProyectile>();
            PlayerProyectile standardLeft = leftProyectile.GetComponent<PlayerProyectile>();

            standardCentral.Shoot(centralPool.gameObject.transform);
            standardRight.Shoot(rightPool.gameObject.transform);
            standardLeft.Shoot(leftPool.gameObject.transform);

            atShoot?.Invoke();
        }

    }

    /// <summary>
    /// Shoot triple cannon using enemy
    /// proyectiles.
    /// </summary>
    public void ShootFromEnemy()
    {
        if (centralCannon != null && rightCannon != null && leftCannon != null)
        {
            GameObject centralProyectile = centralPool.SpawnPrefab(centralCannon.localPosition);
            GameObject rightProyectile = rightPool.SpawnPrefab(rightCannon.localPosition);
            GameObject leftProyectile = leftPool.SpawnPrefab(leftCannon.localPosition);

            if (centralProyectile && rightProyectile && leftProyectile)
            {
                EnemyProyectile standardCentral = centralProyectile.GetComponent<EnemyProyectile>();
                EnemyProyectile standardRight = rightProyectile.GetComponent<EnemyProyectile>();
                EnemyProyectile standardLeft = leftProyectile.GetComponent<EnemyProyectile>();

                standardCentral.Shoot(centralPool.gameObject.transform);
                standardRight.Shoot(rightPool.gameObject.transform);
                standardLeft.Shoot(leftPool.gameObject.transform);

                atShoot?.Invoke();
            }
        }
    }
}
