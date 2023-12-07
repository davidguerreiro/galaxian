using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileData : ScriptableObject
{
    public int id;
    public string baseName;
    public float damage;
    public float speed;
    public float cooling;
    public bool moveDiagonal;
    public string direction;
}
