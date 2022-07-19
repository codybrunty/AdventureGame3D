using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState{
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    private readonly int BlockAnimation = Animator.StringToHash("Block");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;

    public override void Enter() {
        stateMachine.Animator.CrossFadeInFixedTime(BlockAnimation, AnimatorCrossFadeDuration);
        stateMachine.InputReader.EndBlockEvent += EndBlocking;
        stateMachine.Health.SetInvunerable(true);
    }

    public override void Tick(float deltaTime) {
        Move(deltaTime);
    }

    public override void Exit() {
        stateMachine.InputReader.EndBlockEvent -= EndBlocking;
        stateMachine.Health.SetInvunerable(false);
    }

    private void EndBlocking() {
        ReturnToCurrentMoveState();
    }

}
