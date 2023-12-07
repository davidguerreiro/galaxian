using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRockSwarm : MonoBehaviour
{
    public GameObject throwRocks;

    /// <summary>
    /// Spawn swarm of sideWalkers.
    /// </summary>
    /// <param name="spawnersPositions">int[]</param>
    /// <param name="spawners">Spawners</param>
    /// <param name="enemies">GameObject</param>
    /// <param name="board">MovementBoard</param>
    /// <param name="row">int</param>
    /// <param name="rows">int[]</param>
    /// <param name="cols">int[]</param>
    public void SpawnSwarm(int[] spawnersPositions, Spawners spawners, GameObject enemies, MovementBoard board, int row, int[] rows, int[] cols)
    {
        for (int i = 0; i < spawnersPositions.Length; i++)
        {
            Transform spawn = spawners.GetSpawner("top", spawnersPositions[i]);

            GameObject enemyGO = Instantiate(throwRocks);

            enemyGO.transform.position = spawn.position;
            enemyGO.transform.parent = enemies.transform;

            // init sidewalker.
            ThrowingRocks instaThrowingRocks = enemyGO.GetComponent<ThrowingRocks>();

            Transform movementPoint = board.GetMovementPoint(row, spawnersPositions[i]);

            instaThrowingRocks.Spawn(movementPoint, board, rows, cols);
        }
    }
}
