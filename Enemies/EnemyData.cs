using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject
{
    public int id;
    public string baseName;
    public float hp;
    public float speed;
    public int collisionDamage;
    public int scoreGiven;
    public bool isBoss;
}
