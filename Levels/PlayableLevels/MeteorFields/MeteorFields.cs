using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFields : LevelEvents
{
    [Header("Enemies")]
    public WhiteWalkerSwarm whiteWalkers;   
    public StickyDownSwarm stickyDowns;
    public SimpleMoveCannonSwarm simpleCannonMoves;
    public ThrowingRockSwarm throwingRocks;

    [Header("Obstacles")]
    public GameObject failingMeteo;

    [Header("BombSpawners")]
    public BombSpawner bombSpawnerLeft;
    public BombSpawner bombSpawnerRight;

    private Coroutine _spawnObstacles;

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
    public IEnumerator LevelLoop()
    {
        Init();

        yield return new WaitForSeconds(7f);

        // first meteo obstacles.
        int[] spawners = new int[4];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 12;
        spawners[3] = 14;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1.5f));

        while (_spawnObstacles != null)
        {
            yield return new WaitForFixedUpdate();
        }

        // first white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // first swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // second white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // second swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // simple cannon.
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // simple cannon victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // second meteor falling wave.
        spawners = new int[8];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 6;
        spawners[3] = 8;
        spawners[4] = 6;
        spawners[5] = 8;
        spawners[6] = 9;
        spawners[7] = 11;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1.5f));

        while (_spawnObstacles != null)
        {
            yield return new WaitForFixedUpdate();
        }

        // third white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // third swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // fourth white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // fourth swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // two simple cannon.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // simple cannon victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // first attackers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        // first sticky downs victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // second meteor falling wave.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 6;
        spawners[3] = 8;
        spawners[4] = 6;
        spawners[5] = 8;
        spawners[6] = 9;
        spawners[7] = 11;
        spawners[8] = 10;
        spawners[9] = 7;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1f));

        while (_spawnObstacles != null)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);

        // two simple cannon.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // second meteor falling wave.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 6;
        spawners[3] = 8;
        spawners[4] = 6;
        spawners[5] = 8;
        spawners[6] = 9;
        spawners[7] = 11;
        spawners[8] = 10;
        spawners[9] = 7;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1.5f));

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // three simple cannon.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[1];
        spawners[0] = 5;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // fifth white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // fifth swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // throw rocks spawm.
        spawners = new int[1];
        spawners[0] = 8;
        int[] rows = { 4, 5, 2, 1 };
        int[] cols = { 2, 8, 13, 8 };

        throwingRocks.SpawnSwarm(spawners, this.spawners, enemies, board, 2, rows, cols);

        // throw rocks victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // three simple cannon with sticky down attackers.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[1];
        spawners[0] = 5;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        spawners = new int[15];
        
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = i;
        }

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 3);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // spawn three rows of stiky attackers.
        for (int i = 0; i < 4; i++)
        {
            stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, i);
        }

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // sixth white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // two simple cannon with throwing rocks enemy.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // throw rocks spawm.
        spawners = new int[1];
        spawners[0] = 8;

        throwingRocks.SpawnSwarm(spawners, this.spawners, enemies, board, 2, rows, cols);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // fourth meteor falling wave.
        spawners = new int[8];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 6;
        spawners[3] = 8;
        spawners[4] = 6;
        spawners[5] = 8;
        spawners[6] = 9;
        spawners[7] = 11;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1.5f));

        yield return new WaitForSeconds(3f);

        spawners = new int[15];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = i;
        }

        // spawn three rows of stiky attackers.
        for (int i = 0; i < 3; i++)
        {
            stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, i);
        }

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // seventh white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // two simple cannon with throwing rocks enemy.
        spawners = new int[2];
        spawners[0] = 2;
        spawners[1] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[15];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = i;
        }


        // spawn three rows of stiky attackers.
        for (int i = 0; i < 3; i++)
        {
            stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, i);
        }

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // height white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // second meteor falling wave.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 4;
        spawners[2] = 6;
        spawners[3] = 8;
        spawners[4] = 6;
        spawners[5] = 8;
        spawners[6] = 9;
        spawners[7] = 11;
        spawners[8] = 10;
        spawners[9] = 7;

        _spawnObstacles = StartCoroutine(SpawnFailingMeteorObstacles(spawners, 1f));

        while (_spawnObstacles != null)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);

        // simple cannon with two throwrocks.
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // throw rocks spawm.
        spawners = new int[1];

        throwingRocks.SpawnSwarm(spawners, this.spawners, enemies, board, 2, rows, cols);

        yield return new WaitForSeconds(1.5f);
        spawners[0] = 14;

        throwingRocks.SpawnSwarm(spawners, this.spawners, enemies, board, 7, rows, cols);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);


        // ninth white walkers swarm.
        spawners = new int[10];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 10;
        spawners[6] = 11;
        spawners[7] = 12;
        spawners[8] = 13;
        spawners[9] = 14;

        whiteWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // init boss battle.
    }

    /// <summary>
    /// Instance failing meteo obstacles.
    /// </summary>
    /// <param name="spawners">int[]</param>
    /// <param name="interval">float</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SpawnFailingMeteorObstacles(int[] spawners, float interval = 1f)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            Transform spawn = this.spawners.GetSpawner("top", spawners[i]);

            GameObject instance = Instantiate(failingMeteo);
            instance.transform.position = spawn.position;

            yield return new WaitForSeconds(interval);
        }

        _spawnObstacles = null;
    }
}
