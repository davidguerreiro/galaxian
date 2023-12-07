using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditionalWeapons : MonoBehaviour
{
    [Header("Status")]
    public string current;
    public int ammo = 40;

    [Header("Weapons")]
    public TripleCannon tripleCannon;
    public WaveCannon waveCannon;
    public TripleCannon tripleWaveCannon;
    public WaveCannon plasmaCannon;

    /// <summary>
    /// Assign weapon.
    /// </summary>
    /// <param name="weaponName">string</param>
    public void AssignWeapon(string weaponName)
    {
        current = weaponName;
        ammo = 40;

        switch (current)
        {
            case "tripleCannon":
                tripleCannon.gameObject.SetActive(true);
                break;
            case "waveCannon":
                waveCannon.gameObject.SetActive(true);
                break;
            case "tripleWaveCannon":
                tripleWaveCannon.gameObject.SetActive(true);
                break;
            case "plasmaCannon":
                plasmaCannon.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Disable additional weapon.
    /// </summary>
    public void DisableWeapon()
    {
        current = "";
        ammo = 0;

        tripleCannon.gameObject.SetActive(false);
        waveCannon.gameObject.SetActive(false);
        tripleWaveCannon.gameObject.SetActive(false);
        plasmaCannon.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shoot current active additional weapon.
    /// </summary>
    public void ShootWeapon()
    {
        if (current != "")
        {
            switch (current)
            {
                case "tripleCannon":
                    tripleCannon.Shoot();
                    break;
                case "waveCannon":
                    waveCannon.Shoot();
                    break;
                case "tripleWaveCannon":
                    tripleWaveCannon.Shoot();
                    break;
                case "plasmaCannon":
                    plasmaCannon.Shoot();
                    break;
                default:
                    break;
            }

            ammo--;

            if (ammo <= 0)
            {
                DisableWeapon();
            }
        }
    }
}
