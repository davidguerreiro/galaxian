using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Enemy
{
    [Header("Rock Settings")]
    public Transform enemyAttached;
    public int inOutDuration;

    /// <summary>
    /// Move in - out.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator MoveInOut()
    {
        int counter = 0;

        // move out.
        while (counter <= inOutDuration)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, enemyAttached.localPosition, (data.speed * Time.deltaTime) * -1);
            yield return new WaitForFixedUpdate();

            counter++;
        }

        counter = 0;

        // move in.
        while (counter <= inOutDuration)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, enemyAttached.localPosition, data.speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();

            counter++;
        }
    }
}
