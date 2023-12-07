using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectile : MonoBehaviour
{
    [Header("Data")]
    public EnemyProyectileData data;

    private bool _shooted;
    private Transform _parentObjectPool;

    // Update is called once per frame
    void Update()
    {
        if (_shooted)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// Move proyectile down at base speed
    /// vertically.
    /// </summary>
    public void MoveDown()
    {
        Vector2 current = transform.position;
        float x = current.x;

        if (data.moveDiagonal)
        {
            if (data.direction == "right")
            {
                x = current.x += (data.speed * Time.deltaTime) / 2.5f;
            } else
            {
                x = current.x -= (data.speed * Time.deltaTime) / 2.5f;
            }
        }

        transform.position = new Vector2(x, current.y -= (data.speed * Time.deltaTime));
    }

    /// <summary>
    /// Remove proyectile from game scene.
    /// Return back to enemy object pool.
    /// </summary>
    public void Remove()
    {
        if (!data.noDestroyOnContact) { 

            if (_parentObjectPool != null && !data.noDestroyOnContact)
            {
                transform.parent = _parentObjectPool;
            }

            _shooted = false;

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Shoot proyectile.
    /// </summary>
    /// <param name="objectPool">Transform</param>
    public void Shoot(Transform objectPool)
    {
        _parentObjectPool = objectPool;
        transform.parent = null;
        _shooted = true;
    }

    /// <summary>
    /// Get proyectile damage.
    /// </summary>
    /// <returns>int</returns>
    public int GetDamage()
    {
        return data.damage;
    }

    /// <summary>
    /// Trigger logic for trigger enter collision.
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RemoveItems"))
        {
            Remove();
        }
    }
}
