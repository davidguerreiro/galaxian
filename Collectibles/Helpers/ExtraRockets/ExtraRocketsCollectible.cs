using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraRocketsCollectible : MonoBehaviour
{
    [Header("Settings")]
    public int rocketsGot;
    public int audioToPlay;

    [Header("Components")]
    public GameObject animRight;
    public GameObject animLeft;

    /// <summary>
    /// Collect extra life logic.
    /// </summary>
    /// <param name="playerObject">GameObject</param>
    /// <param name="audio">AudioComponent</param>
    public void Collect(GameObject playerObject, AudioComponent audio)
    {
        Player player = playerObject.GetComponent<Player>();
        player.UpdateRockets(rocketsGot);

        if (animRight != null)
        {
            animRight.SetActive(false);
        }

        if (animLeft != null)
        {
            animLeft.SetActive(false);
        }

        audio.PlaySound(audioToPlay);
    }
}
