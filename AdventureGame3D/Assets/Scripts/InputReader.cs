using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions{

    public Vector2 MovementValue { get; private set; }
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action AttackEvent;
    public event Action HeavyAttackEvent;
    public event Action BlockEvent;
    public event Action EndBlockEvent;

    private Controls controls;

    private void Start() {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy() {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context) {
        if (!context.performed) { return; }
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context) {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) {
        
    }

    public void OnTarget(InputAction.CallbackContext context) {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (!context.performed) { return; }
        AttackEvent?.Invoke();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context) {
        if (!context.performed) { return; }
        HeavyAttackEvent?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context) {
        if (context.started) {
            BlockEvent?.Invoke();
        }
        if (context.canceled) {
            EndBlockEvent?.Invoke();
        }
    }
}
