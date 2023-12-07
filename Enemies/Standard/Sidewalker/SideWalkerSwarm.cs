using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkerSwarm : MonoBehaviour
{
   public GameObject sideWalker;

    /// <summary>
    /// Spawn swarm of sideWalkers.
    /// </summary>
    /// <param name="spawnersPositions">int[]</param>
    /// <param name="spawners">Spawners</param>
    /// <param name="enemies">GameObject</param>
    /// <param name="board">MovementBoard</param>
   public void SpawnSwarm(int[] spawnersPositions, Spawners spawners, GameObject enemies, MovementBoard board)
   {
        for (int i = 0; i < spawnersPositions.Length; i++)
        {
            Transform spawn = spawners.GetSpawner("top", spawnersPositions[i]);

            GameObject enemyGO = Instantiate(sideWalker);

            enemyGO.transform.position = spawn.position;
            enemyGO.transform.parent = enemies.transform;

            // init sidewalker.
            Sidewalker instaSidewalker = enemyGO.GetComponent<Sidewalker>();

            int row = (i % 2 == 0) ? 1 : 2;
            Transform movementPoint = board.GetMovementPoint(row, spawnersPositions[i]);

            instaSidewalker.Spawn(movementPoint, board, row);
        }
    }
}
