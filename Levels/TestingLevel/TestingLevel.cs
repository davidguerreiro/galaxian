using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLevel : LevelEvents
{
    [Header("Enemies")]
    public GameObject sideWalker;
    public GameObject arturus;


    /// <summary>
    /// Init level loop.
    /// </summary>
    public void InitLevel()
    {
        if (_levelLoop == null)
        {
            _levelLoop = StartCoroutine(LevelLoop());
        }
    }

    /// <summary>
    /// Level loop.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator LevelLoop()
    {
        Init();

        yield return new WaitForSeconds(7f);

        // first swarm.
        int[] spawners = new int[3];
        spawners[0] = 4;
        spawners[1] = 8;
        spawners[2] = 12;

        for (int i = 0; i < spawners.Length; i++) {
            Transform spawn = this.spawners.GetSpawner("top", spawners[i]);

            GameObject enemyGO = Instantiate(sideWalker);

            enemyGO.transform.position = spawn.position;
            enemyGO.transform.parent = enemies.transform;

            // init sidewalker.
            Sidewalker instaSidewalker = enemyGO.GetComponent<Sidewalker>();

            int row = (i % 2 == 0) ? 1 : 2;
            Transform movementPoint = board.GetMovementPoint(row, spawners[i]);

            instaSidewalker.Spawn(movementPoint, board, row);
        }

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // start boss battle.
        _bossBattle = StartCoroutine(BossBattle());

        while (_bossBattle != null)
        {
            yield return new WaitForFixedUpdate();
        }

        // end level.
        gameManager.levelManager.EndLevel();
    }

    /// <summary>
    /// Boss battle coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator BossBattle()
    {
        gameManager.levelManager.FadeLevelMusic();
        yield return new WaitForSeconds(3f);

        Transform spawn = spawners.GetSpawner("top", 8);
        GameObject bossInstance = Instantiate(this.arturus);

        bossInstance.transform.position = spawn.position;

        // init arturus boss.
        Arturus arturus = bossInstance.GetComponent<Arturus>();

        arturus.Spawn(bossStart.transform, board);

        yield return new WaitForSeconds(3f);

        gameManager.levelManager.PlayeLevelMusic(1);
        arturus.SetReady();

        while (arturus != null)
        {
            yield return new WaitForFixedUpdate();
        }

        _bossBattle = null;
    }
}
