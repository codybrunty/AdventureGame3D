using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState{

    private Attack attack;
    private int forceTimesIndex;

    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) {
        attack = stateMachine.Attacks[attackIndex];
        stateMachine.WeaponDamage.SetDamage(attack.Damage);
    }

    public override void Enter() {
        stateMachine.InputReader.AttackEvent += TryComboAttack;
        stateMachine.InputReader.HeavyAttackEvent += TryComboHeavyAttack;
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,0.1f);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        FaceAttacking(deltaTime);
        TryApplyForce();
        if (GetNormalizedTime() >= 1f) {
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
        stateMachine.InputReader.HeavyAttackEvent -= TryComboHeavyAttack;
    }

    private float GetNormalizedTime() {
        AnimatorStateInfo currentState = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(attack.AnimationName)) {
            return currentState.normalizedTime;
        }
        else {
            return 0f;
        }
    }

    private void TryComboAttack() {
        if (attack.ComboStateIndex == -1) { return; }
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ComboAttackTime) {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, attack.ComboStateIndex));
        }
    }
    private void TryComboHeavyAttack() {
        if (attack.ComboStateIndex != -1) { return; }
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ComboAttackTime) {
            stateMachine.SwitchState(new PlayerHeavyAttackState(stateMachine, 0));
        }
    }
    private void TryApplyForce() {
        if (forceTimesIndex == attack.Force.Count) { return; }
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ForceTimes[forceTimesIndex]) {
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force[forceTimesIndex]);
            forceTimesIndex++;
        }
    }
    protected void FaceAttacking(float deltaTime) {
        if (stateMachine.Targeter.CurrentTarget == null) {
            float normalizedTime = GetNormalizedTime();
            if (normalizedTime < attack.ComboAttackTime) {
                FaceMovementDirection(CalculateMovement(), deltaTime);
            }
        }
        else {
            FaceTarget();
        }
    }
}
