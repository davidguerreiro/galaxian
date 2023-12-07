using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesUI : MonoBehaviour
{
    private TextComponent _text;
    private Player _player;

    /// <summary>
    /// Uddate runs per frame.
    /// </summary>
    private void Update()
    {
        if (_player != null)
        {
            UpdatePlayerLifes();
        }
    }

    /// <summary>
    /// Update player lifes.
    /// </summary>
    private void UpdatePlayerLifes()
    {
        _text.UpdateContent(_player.lifes.ToString());
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="player">Player</param>
    public void Init(Player player)
    {
        _player = player;
        _text = GetComponent<TextComponent>();
    }
}
