using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState{

    private Attack attack;
    private int forceTimesIndex;
    
    public PlayerHeavyAttackState(PlayerStateMachine stateMachine,int attackIndex) : base(stateMachine) {
        attack = stateMachine.HeavyAttacks[attackIndex];
        stateMachine.WeaponDamage.SetAttack(attack.Damage, attack.Knockback);
    }

    public override void Enter() {
        stateMachine.InputReader.AttackEvent += TryComboAttack;
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        FaceAttacking(deltaTime);
        TryApplyForce();
        if (GetNormalizedTime(stateMachine.Animator, attack.AnimationName) >= 1f) {
            if (stateMachine.Targeter.CurrentTarget == null) {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            else {
                stateMachine.SwitchState(new PlayerTargetState(stateMachine));
            }
        }
    }

    public override void Exit() {
        stateMachine.InputReader.AttackEvent -= TryComboAttack;
    }

    private void TryComboAttack() {
        if (attack.ComboStateIndex != -1) { return; }
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, attack.AnimationName);

        if (normalizedTime >= attack.ComboAttackTime) {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
        }
    }

    private void TryApplyForce() {
        if (forceTimesIndex==attack.Force.Count) { return; }
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, attack.AnimationName);

        if (normalizedTime >= attack.ForceTimes[forceTimesIndex]) {
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force[forceTimesIndex]);
            forceTimesIndex++;
        }
    }
    protected void FaceAttacking(float deltaTime) {
        if (stateMachine.Targeter.CurrentTarget == null) {
            float normalizedTime = GetNormalizedTime(stateMachine.Animator, attack.AnimationName);
            if (normalizedTime < attack.ComboAttackTime) {
                FaceMovementDirection(CalculateMovement(), deltaTime);
            }
        }
        else {
            FaceTarget();
        }
    }
}
