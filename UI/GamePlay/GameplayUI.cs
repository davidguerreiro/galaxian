using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("UI Components")]
    public LifesUI lifesUI;
    public ScoreUI scoreUI;
    public EnergyUI energyUI;
    public RocketsUI rocketsUI;
    public AdditionalWeaponUI additionalWeaponsUI;
    public ShieldUI shieldUI;
    public GameObject gameOverMenu;
    
    private Player _player;

    /// <summary>
    /// Init dependencies.
    /// </summary>
    /// <param name="player">Player</param>
    public void InitDependencies(Player player)
    {
        _player = player;

        // init lifes counter.
        lifesUI.Init(_player);

        // init score counter.
        scoreUI.Init(_player);

        // init energy UI.
        energyUI.Init(_player);

        // init rockets UI.
        rocketsUI.Init(_player);

        // init additional weapons UI.
        additionalWeaponsUI.Init(_player.playerCombat);

        // init shield UI.
        shieldUI.Init(_player.playerCombat);
    }

    /// <summary>
    /// Display game over menu.
    /// </summary>
    public void DisplayGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
