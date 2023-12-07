using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEnergyCollectable : MonoBehaviour
{
    [Header("Settings")]
    public int energyGot;
    public int audioToPlay;

    /// <summary>
    /// Collect extra life logic.
    /// </summary>
    /// <param name="playerObject">GameObject</param>
    /// <param name="audio">AudioComponent</param>
    public void Collect(GameObject playerObject, AudioComponent audio)
    {
        Player player = playerObject.GetComponent<Player>();
        player.UpdateHealth(energyGot);

        audio.PlaySound(audioToPlay);
    }
}
