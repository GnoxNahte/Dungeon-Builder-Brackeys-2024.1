using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Library", menuName = "ScriptableObjs/Tile Library")]
public class TileLibrary : ScriptableObject
{
    public enum TileType
    {
        Trap,
        Spawner,
        Enemy, 
        Decoration,

    }
    [SerializeField] TileBase[] tiles;
}
