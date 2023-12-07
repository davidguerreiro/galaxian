using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    public GameObject[] energy = new GameObject[5];
    private Player _player;

    /// <summary>
    /// Render player energy in
    /// the UI.
    /// </summary>
    public void RenderEnergy()
    {
        foreach (GameObject bar in energy)
        {
            bar.SetActive(false);
        }

        for (int i = 0; i < _player.health; i++)
        {
            energy[i].SetActive(true);
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="player">Player</param>
    public void Init(Player player)
    {
        _player = player;
        RenderEnergy();
    }

}
