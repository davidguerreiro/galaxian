using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalWeaponUI : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite tripleCannon;
    public Sprite waveCannon;
    public Sprite tripleWaveCannon;
    public Sprite plasmaCannon;

    private Image _image;
    private PlayerCombat _playerCombat;

    // Update is called once per frame
    void Update()
    {
        if (_playerCombat != null)
        {
            UpdateWeaponSprite();
        }
    }

    /// <summary>
    /// Set weapon sprite.
    /// </summary>
    private void UpdateWeaponSprite()
    {
        switch (_playerCombat.additionalWeapons.current)
        {
            case "tripleCannon":
                _image.sprite = tripleCannon;
                break;
            case "waveCannon":
                _image.sprite = waveCannon;
                break;
            case "tripleWaveCannon":
                _image.sprite = tripleWaveCannon;
                break;
            case "plasmaCannon":
                _image.sprite = plasmaCannon;
                break;
            default:
                _image.sprite = defaultSprite;
                break;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="playerCombat">PlayerCombat</param>
    public void Init(PlayerCombat playerCombat)
    {
        _image = GetComponent<Image>();
        _playerCombat = playerCombat;
    }
}
