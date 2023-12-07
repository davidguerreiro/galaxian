using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveCannonSwarm : MonoBehaviour
{
    public GameObject simpleCannonMove;

    /// <summary>
    /// Spawn swarm of sideWalkers.
    /// </summary>
    /// <param name="spawnersPositions">int[]</param>
    /// <param name="spawners">Spawners</param>
    /// <param name="enemies">GameObject</param>
    /// <param name="board">MovementBoard</param>
    /// <param name="row">int</param>
    public void SpawnSwarm(int[] spawnersPositions, Spawners spawners, GameObject enemies, MovementBoard board, int row = 1)
    {
        for (int i = 0; i < spawnersPositions.Length; i++)
        {
            Transform spawn = spawners.GetSpawner("top", spawnersPositions[i]);

            GameObject enemyGO = Instantiate(simpleCannonMove);

            enemyGO.transform.position = spawn.position;
            enemyGO.transform.parent = enemies.transform;

            // init sidewalker.
            SimpleMoveCannon instaSimple = enemyGO.GetComponent<SimpleMoveCannon>();

            Transform movementPoint = board.GetMovementPoint(row, spawnersPositions[i]);

            instaSimple.Spawn(movementPoint, board, row);
        }
    }
}
