using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [Header("Components")]
    public GameObject shieldImageUI;

    private PlayerCombat _playerCombat;

    // Update is called once per frame
    void Update()
    {
        if (_playerCombat != null)
        {
            RenderShieldSprite();
        }
    }

    private void RenderShieldSprite()
    {
        shieldImageUI.SetActive(_playerCombat.shieldActive);
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init(PlayerCombat playerCombat)
    {
        _playerCombat = playerCombat;
    }
}
