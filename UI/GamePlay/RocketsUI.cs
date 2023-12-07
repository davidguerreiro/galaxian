using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsUI : MonoBehaviour
{
    private Player _player;
    private TextComponent _text;

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            UpdatePlayerRockets();
        }
    }

    /// <summary>
    /// Update player rockets quantity in the
    /// UI.
    /// </summary>
    private void UpdatePlayerRockets()
    {
        _text.UpdateContent(_player.rockets.ToString());
    }

    public void Init(Player player)
    {
        _player = player;
        _text = GetComponent<TextComponent>();
    }
}
