using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState{

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Jump State - Enter");
    }

    public override void Tick(float deltaTime) {
        Debug.Log("Jump State - Tick");
    }

    public override void Exit() {
        Debug.Log("Jump State - Exit");
    }
}
