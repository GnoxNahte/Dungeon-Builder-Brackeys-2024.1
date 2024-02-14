using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MonsterStats
{
    public int health;
    public float moveSpeed;
    public int baseDamage;
    // Attack per second
    public float attackSpeed;
    public string[] skillDescription;
}

[Serializable]
public class ItemInfo
{
    public enum ItemType
    {
        Tile,
        AnimatedTile,
        Monster,
        Spawner,
        Decoration,
    }

    public string Name;
    public ItemType Type;
    [TextArea]
    public string Description;
    public int cost;
    public Sprite icon;
    public Vector2 iconScaleMultiplier = Vector2.one; // For small icons
    // Info if its a tile
    public TileBase tile;
    // Info if its a monster
    public MonsterStats monsterStats;
}
