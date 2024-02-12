using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap dungeonBaseMap;
    [SerializeField] private Tilemap dungeonFloorMap;

    [SerializeField] private RuleTile dungeonRuleTile;
    [SerializeField] private RuleTile dungeonFloorTile;

    [ReadOnly]
    private Vector2Int startPos;

    private InputManager input;
    private Camera cam;

    private void Start()
    {
        input = GameManager.Input;
        cam = Camera.main;
    }

    private void Update()
    {
        if (input.Cursor1Clicked)
        {
            var mouseWorldPos = Vector2Int.RoundToInt(cam.ScreenToWorldPoint(input.CursorPos));

            if (input.Cursor1ClickedThisFrame)
                startPos = Vector2Int.RoundToInt(mouseWorldPos);

            BoundsInt bounds = new BoundsInt((Vector3Int)startPos,(Vector3Int)mouseWorldPos);
            print(bounds);
            print("Size: " + bounds.size.x * bounds.size.y);
            //TileBase[] tileArray = new TileBase[bounds.size.x * bounds.size.y];
            //Array.Fill(tileArray, dungeonRuleTile);
            //dungeonBaseMap.SetTilesBlock(bounds, tileArray);
        }
    }
}
