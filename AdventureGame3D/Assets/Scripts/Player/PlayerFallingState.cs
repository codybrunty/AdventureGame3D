using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int FallingAnimation = Animator.StringToHash("Fall");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;
    private Vector3 momentum;

    public override void Enter() {
        stateMachine.Animator.CrossFadeInFixedTime(FallingAnimation, AnimatorCrossFadeDuration);
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
    }

    public override void Tick(float deltaTime) {
        Move(momentum, deltaTime);
        FaceTarget();
        if (stateMachine.CharacterController.isGrounded) {
            ReturnToCurrentMoveState();
        }
    }

    public override void Exit() {
    }

}
