using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State{

    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime) {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.ForceMovement()) * deltaTime);
    }
    protected void Move(float deltaTime) {
        stateMachine.CharacterController.Move((stateMachine.ForceReceiver.ForceMovement()) * deltaTime);
    }

    protected void FaceTarget() {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }
        Vector3 targetDirection = (stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position).normalized;
        targetDirection.y = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }

}
