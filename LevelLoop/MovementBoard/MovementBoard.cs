using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoard : MonoBehaviour
{
    public Transform[,] board = new Transform[8,15];
       
    /// <summary>
    /// Init board references.
    /// </summary>
    public void InitBoard()
    {
        int i = 0;
        int j = 0;

        foreach (Transform row in gameObject.transform)
        {
            foreach (Transform block in row)
            {
                board[i, j] = block;
                j++;
            }

            j = 0;
            i++;
        }
    }

    /// <summary>
    /// Get movement point to be used be game
    /// logic.
    /// </summary>
    /// <param name="row">int</param>
    /// <param name="column">int</param>
    /// <returns>Transform</returns>
    public Transform GetMovementPoint(int row, int column)
    {
        return board[row, column];
    }
}
