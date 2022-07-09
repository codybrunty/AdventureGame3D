using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState{

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;

    public override void Enter() {
        stateMachine.InputReader.TargetEvent += Target;
    }

    public override void Tick(float deltaTime) {
        Vector3 movement = CalculateMovement();
        stateMachine.CharacterController.Move(movement * deltaTime * stateMachine.MovementSpeed_FreeLook);
        if (movement == Vector3.zero) {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement,deltaTime);
    }

    public override void Exit() {
        stateMachine.InputReader.TargetEvent -= Target;
    }

    private void Target() {
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }

    private Vector3 CalculateMovement() {
        Vector3 camera_forward = new Vector3(stateMachine.MainCameraTransform.forward.x, 0f, stateMachine.MainCameraTransform.forward.z);
        Vector3 camera_right = new Vector3(stateMachine.MainCameraTransform.right.x, 0f, stateMachine.MainCameraTransform.right.z);
        return camera_forward.normalized * stateMachine.InputReader.MovementValue.y + camera_right.normalized * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime) {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,Quaternion.LookRotation(movement),deltaTime * stateMachine.RotationDamping);
    }

}
