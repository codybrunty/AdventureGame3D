using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState{

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int JumpAnimation = Animator.StringToHash("Jump");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;
    private Vector3 momentum;

    public override void Enter() {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(JumpAnimation, AnimatorCrossFadeDuration);
    }

    public override void Tick(float deltaTime) {
        Move(momentum, deltaTime);
        FaceTarget();

        if(stateMachine.CharacterController.velocity.y <= 0) {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    public override void Exit() {
    }

}
