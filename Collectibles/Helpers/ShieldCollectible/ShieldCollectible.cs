using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectible : MonoBehaviour
{
    /// <summary>
    /// Collect extra life logic.
    /// </summary>
    /// <param name="playerObject">GameObject</param>
    public void Collect(GameObject playerObject)
    {
        PlayerCombat playerCombat = playerObject.GetComponent<PlayerCombat>();
        playerCombat.EnableShield();
    }
}
