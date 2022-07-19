using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState{

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

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
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit() {
    }


}
