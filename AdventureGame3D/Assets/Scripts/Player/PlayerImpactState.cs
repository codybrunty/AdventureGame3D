using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState{
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int ImpactAnimation = Animator.StringToHash("Impact");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;
    private float impactDuration = 1f;
    public override void Enter() {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactAnimation, AnimatorCrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        impactDuration -= deltaTime;
        if (impactDuration <= 0f) {
            ReturnToCurrentMoveState();
        }
    }

    public override void Exit() {
    }

}
