using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] lootItems;

    [Header("Settings")]
    public int chance;

    /// <summary>
    /// Instance loot item.
    /// </summary>
    public void InstanceLoot()
    {
        GameObject loot = GetLootItem();

        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Generate loot.
    /// </summary>
    /// <returns>GameObject</returns>
    private GameObject GetLootItem()
    {
        GameObject loot = null;

        if (Random.Range(0, 100) <= chance)
        {
            int index = Random.Range(0, lootItems.Length);
            loot = lootItems[index];
        }

        return loot;
    }
}
