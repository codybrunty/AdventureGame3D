using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State{

    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }
    protected bool IsInChaseRange() {
        bool results = false;

        float mag = Vector3.Magnitude(stateMachine.Player.transform.position - stateMachine.transform.position);
        if (mag < stateMachine.PlayerChasingRange) {
            results = true;
        }
        if (stateMachine.Player.IsDead) {
            results = false;
        }
        return results;
    }
    protected bool IsInAttackRange() {
        bool results = false;

        float mag = Vector3.Magnitude(stateMachine.Player.transform.position - stateMachine.transform.position);
        if (mag < stateMachine.AttackRange) {
            results = true;
        }
        if (stateMachine.Player.IsDead) {
            results = false;
        }

        return results;
    }

    protected void FacePlayer() {
        if (stateMachine.Player == null) { return; }
        Vector3 targetDirection = (stateMachine.Player.transform.position - stateMachine.transform.position).normalized;
        targetDirection.y = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }
    protected void Move(Vector3 motion, float deltaTime) {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.ForceMovement()) * deltaTime);
    }
    protected void Move(float deltaTime) {
        stateMachine.CharacterController.Move((stateMachine.ForceReceiver.ForceMovement()) * deltaTime);
    }
}
