using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private Player _player;
    private TextComponent _text;

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            UpdatePlayerScore();
        }
    }

    /// <summary>
    /// Update player score in the UI.
    /// </summary>
    private void UpdatePlayerScore()
    {
        _text.UpdateContent(_player.score.ToString());
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
