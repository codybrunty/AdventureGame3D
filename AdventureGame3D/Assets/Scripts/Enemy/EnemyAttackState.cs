using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState{

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    private readonly int AttackAnimation = Animator.StringToHash("Attack");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;

    public override void Enter() {
        stateMachine.WeaponDamage.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackAnimation, AnimatorCrossFadeDuration);
    }

    public override void Tick(float deltaTime) {
        FacePlayer();
        if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1) {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    public override void Exit() {
    }


}
