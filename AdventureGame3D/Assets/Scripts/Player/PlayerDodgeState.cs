using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState{
    public PlayerDodgeState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Dodge State - Enter");
    }

    public override void Tick(float deltaTime) {
        Debug.Log("Dodge State - Tick");
    }

    public override void Exit() {
        Debug.Log("Dodge State - Exit");
    }
}
