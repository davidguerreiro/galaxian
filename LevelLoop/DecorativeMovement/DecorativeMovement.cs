using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeMovement : MonoBehaviour
{
    [Header("Components")]
    public Transform start;
    public Transform finish;

    [Header("Settings")]
    public float speed;
    public float toWaitStart;
    public float toWaitEnd;

    private Coroutine _movementRoutine;

    // Update is called once per frame
    void Update()
    {
        if (_movementRoutine == null)
        {
            _movementRoutine = StartCoroutine(Move());
        }
    }

    /// <summary>
    /// Move decorate towards target.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Move()
    {
        yield return new WaitForSeconds(toWaitStart);

        while (Vector3.Distance(transform.localPosition, finish.localPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, finish.localPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = finish.localPosition;
        yield return new WaitForSeconds(toWaitEnd);
        transform.localPosition = start.localPosition;

        _movementRoutine = null;

    }
}
