using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [SerializeField] float cursorEdgePadding;
    [SerializeField] Vector2 zoomRange;
    [SerializeField] float zoomSpeed;

    private CinemachineBrain cinemachineBrain;
    private InputManager input;

    private CinemachineVirtualCamera activeVirtualCam => ((CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera);

    private void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        input = GameManager.Input;
    }

    private void Update()
    {
        // ====================
        // Panning the camera
        // ====================

#if UNITY_EDITOR
        // For in editor, if leave the game window
        if (input.CursorPos.x <= 0 || input.CursorPos.x >= Screen.width ||
            input.CursorPos.y <= 0 || input.CursorPos.y >= Screen.height)
            return;
#endif

        // Prioritise user input, move and returns immediately if keyboard input is given
        // Priorites based on:
        // 1. Mouse input (RMB / MMB)
        // 2. Keyboard input
        // 3. Mouse on edge of screen

        if (input.Cursor2Clicked || input.Cursor3Clicked)
        {
            transform.position -= (Vector3)input.CursorDelta / 235f * activeVirtualCam.m_Lens.OrthographicSize;
            //Cursor.SetCursor
            return;
        }

        if (input.Move.sqrMagnitude > 0)
        {
            transform.position += (Vector3)input.Move * cameraSpeed;
            return;
        }

        Vector2 totalMoveDelta = input.Move * cameraSpeed;

        if (input.CursorPos.x < cursorEdgePadding)
            totalMoveDelta.x -= cameraSpeed;
        else if (input.CursorPos.x > Screen.width - cursorEdgePadding)
            totalMoveDelta.x += cameraSpeed;

        if (input.CursorPos.y < cursorEdgePadding)
            totalMoveDelta.y -= cameraSpeed;
        else if (input.CursorPos.y > Screen.height - cursorEdgePadding)
            totalMoveDelta.y += cameraSpeed;

        transform.position += (Vector3)totalMoveDelta;

        // ====================
        // Zooming the camera
        // ====================

        if (input.Scroll != 0)
        {
            float newOrthoSize = activeVirtualCam.m_Lens.OrthographicSize - input.Scroll * zoomSpeed;
            newOrthoSize = Mathf.Clamp(newOrthoSize, zoomRange.x, zoomRange.y);
            activeVirtualCam.m_Lens.OrthographicSize = newOrthoSize;
        }
    }
}
