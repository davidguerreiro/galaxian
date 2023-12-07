using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectile : MonoBehaviour
{
    [Header("Data")]
    public ProyectileData data;

    [Header("Settings")]
    public int destroyAt;

    private bool _shooted;
    private AudioComponent _audio;
    private int counter;
    private Transform _parentObjectPool;

    // Update is called once per frame
    void Update()
    {
        if (_shooted)
        {
            MoveUp();
        }
    }

    /// <summary>
    /// Move up proyectile vertically.
    /// </summary>
    public void MoveUp()
    {
        Vector2 current = transform.position;

        float x = current.x;

        if (data.moveDiagonal)
        {
            if (data.direction == "right")
            {
                x = current.x += (data.speed * Time.deltaTime) / 2.5f;
            }
            else
            {
                x = current.x -= (data.speed * Time.deltaTime) / 2.5f;
            }
        }

        transform.position = new Vector2(x, current.y += (data.speed * Time.deltaTime));

        counter++;

        if (counter >= destroyAt)
        {
            Remove();
        }
    }

    /// <summary>
    /// Remove bullet from game screen.
    /// </summary>
    public void Remove()
    {
        transform.parent = _parentObjectPool;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Shoot proyectile.
    /// </summary>
    /// <param name="objectPool">Transform</param>
    public void Shoot(Transform objectPool)
    {
        _parentObjectPool = objectPool;
        counter = 0;

        transform.parent = null;

        if (_audio == null)
        {
            _audio = GetComponent<AudioComponent>();
        }

        _audio.PlaySound();

        _shooted = true;
    }

    /// <summary>
    /// Get bullet damage.
    /// </summary>
    /// <returns>float</returns>
    public float GetDamage()
    {
        return data.damage;
    }

    /// <summary>
    /// Get cooling value to be used
    /// by cannon.
    /// </summary>
    /// <returns>float</returns>
    public float GetCooling()
    {
        return data.cooling;
    }

}
