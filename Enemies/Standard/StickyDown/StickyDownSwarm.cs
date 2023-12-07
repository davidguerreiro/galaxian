using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyDownSwarm : MonoBehaviour
{
    public GameObject stickyDown;

    /// <summary>
    /// Spawn swarm of sideWalkers.
    /// </summary>
    /// <param name="spawnersPositions">int[]</param>
    /// <param name="spawners">Spawners</param>
    /// <param name="enemies">GameObject</param>
    /// <param name="board">MovementBoard</param>
    /// /// <param name="row">int</param>
    public void SpawnSwarm(int[] spawnersPositions, Spawners spawners, GameObject enemies, MovementBoard board, int row = 1)
    {
        for (int i = 0; i < spawnersPositions.Length; i++)
        {
            Transform spawn = spawners.GetSpawner("top", spawnersPositions[i]);

            GameObject enemyGO = Instantiate(stickyDown);

            enemyGO.transform.position = spawn.position;
            enemyGO.transform.parent = enemies.transform;

            // init sidewalker.
            StickyDown instaSidewalker = enemyGO.GetComponent<StickyDown>();

            Transform movementPoint = board.GetMovementPoint(row, spawnersPositions[i]);

            instaSidewalker.Spawn(movementPoint);
        }
    }
}
