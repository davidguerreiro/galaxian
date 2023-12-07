using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailingObstacle : MonoBehaviour
{
    public Obstacle data;

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }

    /// <summary>
    /// Move Dropping bomb down.
    /// </summary>
    public void MoveDown()
    {
        Vector2 current = transform.position;
        transform.position = new Vector2(current.x, current.y -= data.speed * Time.deltaTime);
    }

    /// <summary>
    /// Get collision damage.
    /// </summary>
    /// <returns>int</returns>
    public int GetCollisionDamage()
    {
        return data.collisionDamage;
    }

    /// <summary>
    /// Control enemy trigger collisions.
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RemoveItems"))
        {
            Destroy(gameObject);
        }
    }
}
