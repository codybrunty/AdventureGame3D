using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int MovementBlendTree = Animator.StringToHash("Movement");
    private readonly int EnemySpeed = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;

    public override void Enter() {
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTree, AnimatorCrossFadeDuration);
    }

    public override void Tick(float deltaTime) {
        Move(deltaTime);
        if (IsInChaseRange()) {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(EnemySpeed, 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit() {
    }

}
