using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType
{
    Pointer,
    Grab,
    Grabbing,
    Draw,
}

public class CursorManager : MonoBehaviour
{
    [SerializeField] CursorType startType = CursorType.Pointer;
    [SerializeField] Texture2D pointer;
    [SerializeField] Texture2D grab;
    [SerializeField] Texture2D grabbing;
    [SerializeField] Texture2D draw;

    private void Start()
    {
        SetCursor(startType);
    }

    public void SetCursor(CursorType type)
    {
        Texture2D cursorTexture = null;
        switch (type)
        {
            case CursorType.Pointer: 
                Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.Grab: 
                Cursor.SetCursor(grab, new Vector2(11, 14), CursorMode.Auto);
                break;
            case CursorType.Grabbing: 
                Cursor.SetCursor(grabbing, new Vector2(10, 10), CursorMode.Auto);
                break;
            case CursorType.Draw: 
                Cursor.SetCursor(draw, new Vector2(0, 20), CursorMode.Auto);
                break;
            default: 
                cursorTexture = null; 
                break;
        }

        if (cursorTexture == null)
        {
            Debug.LogError("Cannot find cursor texture");
            return;
        }
    }

    private void OnValidate()
    {
        SetCursor(startType);
    }
}
