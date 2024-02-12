using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private InputAction cursorPos;
    private InputAction cursorDelta;
    private InputAction cursorBtn1;
    private InputAction cursorBtn2;
    private InputAction cursorBtn3;
    private InputAction move;
    private InputAction scroll;

    [field: SerializeField] public Vector2 CursorPos { get; private set; }
    [field: SerializeField] public Vector2 CursorDelta { get; private set; }
    [field: SerializeField] public bool Cursor1Clicked{ get; private set; }
    [field: SerializeField] public bool Cursor1ClickedThisFrame{ get; private set; }
    [field: SerializeField] public bool Cursor2Clicked{ get; private set; }
    [field: SerializeField] public bool Cursor2ClickedThisFrame{ get; private set; }
    [field: SerializeField] public bool Cursor3Clicked{ get; private set; }
    [field: SerializeField] public bool Cursor3ClickedThisFrame{ get; private set; }
    [field: SerializeField] public Vector2 Move{ get; private set; }
    [field: SerializeField] public float Scroll { get; private set; }

    public event Action<InputAction.CallbackContext> OnCursor1Released;

    private void Awake()
    {
        playerControls = new PlayerControls();

        cursorPos = playerControls.Player.CursorPos;
        cursorDelta = playerControls.Player.CursorDelta;
        cursorBtn1 = playerControls.Player.Cursor1Btn;
        cursorBtn2 = playerControls.Player.Cursor2Btn;
        cursorBtn3 = playerControls.Player.Cursor3Btn;

        move = playerControls.Player.Move;
        scroll = playerControls.Player.Scroll;

    }

    private void OnEnable()
    {
        cursorPos.Enable();
        cursorDelta.Enable();
        cursorBtn1.Enable();
        cursorBtn2.Enable();
        cursorBtn3.Enable();

        move.Enable();
        scroll.Enable();

        cursorBtn1.canceled += InvokeEvent;
    }

    private void OnDisable()
    {
        cursorPos.Disable();
        cursorDelta.Disable();
        cursorBtn1.Disable();
        cursorBtn2.Disable();
        cursorBtn3.Disable();

        move.Disable();
        scroll.Disable();

        cursorBtn1.canceled -= InvokeEvent;
    }

    private void Update()
    {
        CursorPos = cursorPos.ReadValue<Vector2>();
        CursorDelta = cursorDelta.ReadValue<Vector2>();

        // Cursors
        Cursor1Clicked = cursorBtn1.IsPressed();
        Cursor1ClickedThisFrame = cursorBtn1.WasPressedThisFrame();

        Cursor2Clicked = cursorBtn2.IsPressed();
        Cursor2ClickedThisFrame = cursorBtn2.WasPressedThisFrame();

        Cursor3Clicked = cursorBtn3.IsPressed();
        Cursor3ClickedThisFrame = cursorBtn3.WasPressedThisFrame();

        Move = move.ReadValue<Vector2>();
        Scroll = scroll.ReadValue<float>();
    }

    // Not sure how to make 
    private void InvokeEvent(InputAction.CallbackContext context) { OnCursor1Released?.Invoke(context); }
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private InputAction cursor;
    private InputAction keyboardMoveDir;

    [field: SerializeField] public Vector2 Aim { get; private set; }
    [field: SerializeField] public Vector2 MoveDir { get; private set; }
    [field: SerializeField] public bool IsDashing { get; private set; }
    [field: SerializeField] public bool IsInteracting { get; private set; }
    [field: SerializeField] public bool IsOpeningInventory { get; private set; }
    [field: SerializeField] public bool IsUsingPrimaryAttack { get; private set; }
    [field: SerializeField] public bool IsUsingSecondaryAttack { get; private set; }
    [field: SerializeField] public bool IsUsingAutoAttack { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();

        aim = playerControls.Player.Aim;
        move = playerControls.Player.Move;
        dash = playerControls.Player.Dash;
        interact = playerControls.Player.Interact;
        inventory = playerControls.Player.Inventory;
        primaryAttack = playerControls.Player.PrimaryAttack;
        secondaryAttack = playerControls.Player.SecondaryAttack;
        autoAttack = playerControls.Player.AutoAttack;
    }

    private void OnEnable()
    {
        aim.Enable();
        move.Enable();
        dash.Enable();
        interact.Enable();
        inventory.Enable();
        primaryAttack.Enable();
        secondaryAttack.Enable();
        autoAttack.Enable();
    }

    private void OnDisable()
    {
        aim.Disable();
        move.Disable();
        dash.Disable();
        interact.Disable();
        inventory.Disable();
        primaryAttack.Disable();
        secondaryAttack.Disable();
        autoAttack.Disable();
    }

    private void Update()
    {
        Aim = aim.ReadValue<Vector2>();
        MoveDir = move.ReadValue<Vector2>();
        IsDashing = dash.WasPressedThisFrame();
        IsInteracting = interact.WasPressedThisFrame();
        IsOpeningInventory = inventory.WasPressedThisFrame();
        IsUsingPrimaryAttack = primaryAttack.WasPressedThisFrame();
        IsUsingSecondaryAttack = secondaryAttack.WasPressedThisFrame();
        IsUsingAutoAttack = autoAttack.WasPressedThisFrame();
    }
}
 */