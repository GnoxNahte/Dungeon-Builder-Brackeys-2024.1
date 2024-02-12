using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Transform overlayTransform;

    [SerializeField] private Tilemap dungeonBaseMap;
    [SerializeField] private Tilemap dungeonFloorMap;

    [SerializeField] private TileBase dungeonRuleTile;
    [SerializeField] private TileBase dungeonFloorTile;

    // Position of cursor, using grid units
    [ReadOnly] [SerializeField]
    private Vector3Int startCursorPos;
    [ReadOnly] [SerializeField]
    private Vector3Int currCursorPos;
    [ReadOnly] [SerializeField]
    private BoundsInt currBounds;

    [ReadOnly] [SerializeField]
    private bool ifCancelPainting;

    private InputManager input;
    private Camera cam;

    private void Start()
    {
        input = GameManager.Input;
        cam = Camera.main;

        input.OnCursor1Released += (context) => SetTiles();
    }

    private void Update()
    {
        if (input.Cursor1ClickedThisFrame)
            ifCancelPainting = false;

        if (input.Cursor1Clicked)
        {
            if (input.Cursor2Clicked)
                ifCancelPainting = true;

            var cursorPos = cam.ScreenToWorldPoint(input.CursorPos);
            currCursorPos = dungeonBaseMap.WorldToCell(cursorPos);

            if (input.Cursor1ClickedThisFrame)
                startCursorPos = currCursorPos;

            currBounds = new BoundsInt(startCursorPos, currCursorPos - startCursorPos);
            if (currBounds.size.x >= 0)
                currBounds.size += Vector3Int.right;
            else
            {
                currBounds.position += Vector3Int.right;
                currBounds.size += Vector3Int.left;
            }

            if (currBounds.size.y >= 0)
                currBounds.size = currBounds.size + Vector3Int.up;
            else
            {
                currBounds.position += Vector3Int.up;
                currBounds.size += Vector3Int.down;
            }
            currBounds.size += Vector3Int.forward;

            overlayTransform.position = currBounds.center;
            overlayTransform.localScale = currBounds.size;

            //print($"Pos: {currBounds.position} | Center: {currBounds.center} | Min: {currBounds.min} | Max: {currBounds.max}");
        }
        else
        {
            ifCancelPainting = true;
        }

        overlayTransform.gameObject.SetActive(!ifCancelPainting);
    }

    private void SetTiles()
    {
        if (ifCancelPainting) 
            return;

        // Not sure why need this, might be a bug in SetTilesBlock where it sets the blocks in the wrong position if size < 0
        BoundsInt editedBounds = currBounds;
        if (currBounds.size.x < 0)
            editedBounds.position -= new Vector3Int(editedBounds.size.x + 1, 0);
        if (currBounds.size.y < 0)
            editedBounds.position -= new Vector3Int(0, editedBounds.size.y + 1);

        // Set walls
        int arraySize = Mathf.Abs(editedBounds.size.x * editedBounds.size.y);
        TileBase[] tileArray = new TileBase[arraySize];
        Array.Fill(tileArray, dungeonRuleTile);
        dungeonBaseMap.SetTilesBlock(editedBounds, tileArray);

        // Set floors
        Array.Fill(tileArray, dungeonFloorTile);
        dungeonFloorMap.SetTilesBlock(editedBounds, tileArray);

        //dungeonBaseMap.ResizeBounds();
        //dungeonBaseMap.CompressBounds();
        //dungeonBaseMap.RefreshAllTiles();
    }
}
