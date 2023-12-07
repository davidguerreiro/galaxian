using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectileData : ScriptableObject
{
    public int id;
    public string baseName;
    public int damage;
    public float speed;
    public bool hasAudio;
    public bool destructible;
    public bool moveDiagonal;
    public string direction;
    public bool noDestroyOnContact;
}
