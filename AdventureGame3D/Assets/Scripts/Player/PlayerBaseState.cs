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
    protected Vector3 CalculateMovement() {
        Vector3 camera_forward = new Vector3(stateMachine.MainCameraTransform.forward.x, 0f, stateMachine.MainCameraTransform.forward.z);
        Vector3 camera_right = new Vector3(stateMachine.MainCameraTransform.right.x, 0f, stateMachine.MainCameraTransform.right.z);
        return camera_forward.normalized * stateMachine.InputReader.MovementValue.y + camera_right.normalized * stateMachine.InputReader.MovementValue.x;
    }
    protected void FaceTarget() {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }
        Vector3 targetDirection = (stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position).normalized;
        targetDirection.y = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    protected void FaceMovementDirection(Vector3 movement, float deltaTime) {
        if (movement == Vector3.zero) { return; }
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

}
