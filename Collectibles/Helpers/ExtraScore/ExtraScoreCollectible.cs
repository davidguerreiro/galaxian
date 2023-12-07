using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScoreCollectible : MonoBehaviour
{
    [Header("Settings")]
    public int scoreGot;
    public int audioToPlay;

    /// <summary>
    /// Collect extra score logic.
    /// </summary>
    /// <param name="playerObject">GameObject</param>
    /// <param name="audio">AudioComponent</param>
    public void Collect(GameObject playerObject, AudioComponent audio)
    {
        Player player = playerObject.GetComponent<Player>();
        player.UpdateScore(scoreGot);

        audio.PlaySound(audioToPlay);
    }
}
