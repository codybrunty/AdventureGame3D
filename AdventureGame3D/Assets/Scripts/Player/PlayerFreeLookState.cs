using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState{

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.25f;

    public override void Enter() {
        stateMachine.InputReader.TargetEvent += Target;
        stateMachine.InputReader.AttackEvent += Attack;
        stateMachine.InputReader.HeavyAttackEvent += HeavyAttack;
        stateMachine.InputReader.BlockEvent += StartBlocking;
        stateMachine.InputReader.JumpEvent += Jump;
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, AnimatorCrossFadeDuration);
    }

    public override void Tick(float deltaTime) {
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.MovementSpeed_FreeLook,deltaTime);
        UpdateAnimator(movement, deltaTime);
        FaceMovementDirection(movement,deltaTime);
    }

    public override void Exit() {
        stateMachine.InputReader.TargetEvent -= Target;
        stateMachine.InputReader.AttackEvent -= Attack;
        stateMachine.InputReader.HeavyAttackEvent -= HeavyAttack;
        stateMachine.InputReader.BlockEvent -= StartBlocking;
        stateMachine.InputReader.JumpEvent -= Jump;
    }

    private void Target() {
        if (stateMachine.Targeter.SelectTarget()) {
            stateMachine.SwitchState(new PlayerTargetState(stateMachine));
        }
    }

    private void UpdateAnimator(Vector3 movement, float deltaTime) {
        if (movement == Vector3.zero) {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }
        float value = Mathf.Abs(stateMachine.InputReader.MovementValue.x) + Mathf.Abs(stateMachine.InputReader.MovementValue.y);
        if(value > .6) {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        }
        else {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.5f, AnimatorDampTime, deltaTime);
        }
    }

    private void Attack() {
        stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
    }
    private void HeavyAttack() {
        stateMachine.SwitchState(new PlayerHeavyAttackState(stateMachine, 0));
    }
    private void StartBlocking() {
        stateMachine.SwitchState(new PlayerBlockState(stateMachine));
    }
    private void Jump() {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

}
