using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingBomb : Enemy
{
    private bool _ready;

    // Update is called once per frame
    void Update()
    {
        if (active && _ready)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// Spawn dropping bomm.
    /// </summary>
    public void Spawn()
    {
        Init();

        _ready = true;
    }

    /// <summary>
    /// Move Dropping bomb down.
    /// </summary>
    public void MoveDown()
    {
        Vector2 current = transform.position;
        transform.position = new Vector2(current.x, current.y -= data.speed * Time.deltaTime);
    }
}
