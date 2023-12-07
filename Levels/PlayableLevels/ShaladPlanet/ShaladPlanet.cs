using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaladPlanet : LevelEvents
{
    [Header("Enemies")]
    public SideWalkerSwarm sideWalkers;
    public StickyDownSwarm stickyDowns;
    public SimpleMoveCannonSwarm simpleCannonMoves;

    [Header("Events")]
    public GameObject[] allyShips;
    public GameObject bossEntryAnim;

    [Header("BombSpawners")]
    public BombSpawner bombSpawnerLeft;
    public BombSpawner bombSpawnerRight;

    [Header("Boss")]
    public GameObject arturus;

    private AudioComponent _audio;

    /// <summary>
    /// Init level loop.
    /// </summary>
    public void InitLevel()
    {
        _audio = GetComponent<AudioComponent>();

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

        // display companions scene.
        DisplayAllyShips();
        yield return new WaitForSeconds(6f);
        HideAllShips();

        // sidewalkers swarm.
        int[] spawners = new int[8];
        spawners[0] = 3;
        spawners[1] = 4;
        spawners[2] = 5;
        spawners[3] = 6;
        spawners[4] = 11;
        spawners[5] = 12;
        spawners[6] = 13;
        spawners[7] = 14;

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // first swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2.5f);

        // sidewalkers swarm.
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

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // second swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // sidewalkers swarm.
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

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // third swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // point attackers swarm.
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

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // second attackers swarm.
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

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // fourth swarm.
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

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // fourth swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // frist combine swarm.
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

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        spawners = new int[6];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 10;
        spawners[4] = 11;
        spawners[5] = 12;

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 4);

        // combine swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // simple cannon.
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        // simple cannon victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // fifth swarm.
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

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // fifth swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // simple cannon with stickt attackers
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[6];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 10;
        spawners[4] = 11;
        spawners[5] = 12;

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 3);

        // combine swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // sixth swarm.
        spawners = new int[12];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 7;
        spawners[6] = 9;
        spawners[7] = 10;
        spawners[8] = 11;
        spawners[9] = 12;
        spawners[10] = 13;
        spawners[11] = 14;

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // sixth swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // two simple cannon with stickt attackers
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[1];
        spawners[0] = 3;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[6];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 10;
        spawners[4] = 11;
        spawners[5] = 12;

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 3);

        // combine swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(2f);

        // third attackers swarm.
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

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // forth attackers swarm.
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // fifth attackers swarm.
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        // seventh swarm.
        spawners = new int[12];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 7;
        spawners[6] = 9;
        spawners[7] = 10;
        spawners[8] = 11;
        spawners[9] = 12;
        spawners[10] = 13;
        spawners[11] = 14;

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // seventh swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // three simple cannon with stickt attackers
        spawners = new int[1];
        spawners[0] = 6;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[1];
        spawners[0] = 8;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 2);

        spawners = new int[1];
        spawners[0] = 3;

        simpleCannonMoves.SpawnSwarm(spawners, this.spawners, enemies, board, 1);

        spawners = new int[6];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 10;
        spawners[4] = 11;
        spawners[5] = 12;

        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 2);
        stickyDowns.SpawnSwarm(spawners, this.spawners, enemies, board, 3);

        // three simple cannons swarm victory condition.
        do
        {
            yield return new WaitForFixedUpdate();
        } while (enemies.transform.childCount > 0);

        yield return new WaitForSeconds(1f);

        // height swarm.
        spawners = new int[12];
        spawners[0] = 2;
        spawners[1] = 3;
        spawners[2] = 4;
        spawners[3] = 5;
        spawners[4] = 6;
        spawners[5] = 7;
        spawners[6] = 9;
        spawners[7] = 10;
        spawners[8] = 11;
        spawners[9] = 12;
        spawners[10] = 13;
        spawners[11] = 14;

        sideWalkers.SpawnSwarm(spawners, this.spawners, enemies, board);

        // height swarm victory condition.
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
    /// Display ally ships.
    /// </summary>
    private void DisplayAllyShips()
    {
        _audio.PlaySound(0);

        for (int i = 0; i < allyShips.Length; i++)
        {
            allyShips[i].SetActive(true);
        }
    }

    /// <summary>
    /// Hide ally ships.
    /// </summary>
    private void HideAllShips()
    {
        for (int i = 0; i < allyShips.Length; i++)
        {
            allyShips[i].SetActive(false);
        }
    }

    /// <summary>
    /// Boss battle coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator BossBattle()
    {
        gameManager.levelManager.FadeLevelMusic();
        yield return new WaitForSeconds(3f);

        // play boss entry anim.
        _audio.PlaySound(0);
        bossEntryAnim.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        bossEntryAnim.SetActive(false);


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
