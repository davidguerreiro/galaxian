using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeCollectable : MonoBehaviour
{
    [Header("Settings")]
    public int audioToPlay;

    /// <summary>
    /// Collect extra life logic.
    /// </summary>
    /// <param name="playerObject">GameObject</param>
    /// <param name="audio">AudioComponent</param>
    public void Collect(GameObject playerObject, AudioComponent audio)
    {
        Player player = playerObject.GetComponent<Player>();
        player.GetLife();

        audio.PlaySound(audioToPlay);
    }
}
