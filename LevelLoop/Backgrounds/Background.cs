using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Settings")]
    public bool canMove;

    private Vector3 _originalPosition;
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    /// <summary>
    /// Move background verically.
    /// </summary>
    public void Move()
    {
        Vector3 position = transform.localPosition;
        transform.localPosition = new Vector3(position.x, position.y -= _speed * Time.deltaTime, position.z);
    }

    /// <summary>
    /// Stop background movement.
    /// </summary>
    public void StopMovement()
    {
        canMove = false;
    }

    /// <summary>
    /// Reset background;
    /// </summary>
    public void ResetBackground()
    {
        transform.localPosition = _originalPosition;
    }

    /// <summary>
    /// Collision detection.
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResetBackground"))
        {
            ResetBackground();
        }
    }

    /// <summary>
    /// Init background.
    /// </summary>
    /// <param name="speed">float</param>
    public void Init(float speed, Vector3 originalPosition)
    {
        _speed = speed;
        canMove = true;
        _originalPosition = originalPosition;
    }
}
