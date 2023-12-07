using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject droppingBomb;

    [Header("Settings")]
    public float delay;
    public float interval;

    private bool _canSpawn;
    private Coroutine _spawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        if (_canSpawn && _spawn == null)
        {
            _spawn = StartCoroutine(SpawnBomb());
        }
    } 

    /// <summary>
    /// Spawn bomb into game scene.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnBomb()
    {
        GameObject bombObject = Instantiate(droppingBomb, transform.position, Quaternion.identity);

        DroppingBomb bomb = bombObject.GetComponent<DroppingBomb>();
        bomb.Spawn();

        yield return new WaitForSeconds(interval);

        _spawn = null;
    }

    /// <summary>
    /// Stop spawming.
    /// </summary>
    public void StopSpawming()
    {
        if (_spawn != null)
        {
            StopCoroutine(_spawn);
            _spawn = null;
        }

        _canSpawn = false;
    }

    /// <summary>
    /// Resume spawning coroutines.
    /// </summary>
    public void ResumeSpawming()
    {
        _canSpawn = true;
        _spawn = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Init()
    {
        yield return new WaitForSeconds(delay);
        _canSpawn = true;
    }
}
