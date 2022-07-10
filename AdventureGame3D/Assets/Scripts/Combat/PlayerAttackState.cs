using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState{

    private float previousFrameTime;
    private Attack attack;
    private bool forceAdded = false;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter() {
        Debug.Log(attack.AnimationName);
        stateMachine.InputReader.AttackEvent += TryComboAttack;
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,0.1f);
    }
    public override void Tick(float deltaTime) {
        Move(deltaTime);
        FaceTarget();
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

    private void Attack() {

    }

    private void TryComboAttack() {
        if (attack.ComboStateIndex == -1) { return; }
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ComboAttackTime) {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, attack.ComboStateIndex));
        }
    }
    private void TryApplyForce() {
        if (forceAdded) { return; }
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ForceTime) {
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
            forceAdded = true;
        }
    }

}
