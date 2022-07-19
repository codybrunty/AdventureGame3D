using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.WeaponDamage.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }
    public override void Tick(float deltaTime) {
    }
    public override void Exit() {
    }

}
