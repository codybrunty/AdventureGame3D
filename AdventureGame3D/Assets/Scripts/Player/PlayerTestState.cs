using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState{

    float t;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime) {
        t += deltaTime;
        Debug.Log(t);
    }

    public override void Exit() {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    public void OnJump() {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
