using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

    [CreateAssetMenu]
public class Crops : ScriptableObject
{
    public int damage;
    public int health;
    public Tile sprite;
    public int stage = 3;
    
    public Tile[] grow_bottom;
    public Tile grow_top;

    public Vector3Int pos;
    
}
