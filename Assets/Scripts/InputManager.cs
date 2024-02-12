using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private InputAction aim;
    private InputAction move;
    private InputAction dash;
    private InputAction interact;
    private InputAction inventory;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private InputAction autoAttack;

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