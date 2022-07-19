using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState{
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementBlendTree = Animator.StringToHash("Movement");
    private readonly int EnemySpeed = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.1f;
    private const float AnimatorCrossFadeDuration = 0.1f;

    public override void Enter() {
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTree, AnimatorCrossFadeDuration);
    }
    public override void Tick(float deltaTime) {
        if (!IsInChaseRange()) {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange()) {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();
        stateMachine.Animator.SetFloat(EnemySpeed, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit() {
        if (stateMachine.NavMeshAgent.isOnNavMesh) {
            stateMachine.NavMeshAgent.ResetPath();
            stateMachine.NavMeshAgent.velocity = Vector3.zero;
        }
    }

    private void MoveToPlayer(float deltaTime) {
        if (stateMachine.NavMeshAgent.isOnNavMesh) {
            stateMachine.NavMeshAgent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.NavMeshAgent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.NavMeshAgent.velocity = stateMachine.CharacterController.velocity;
    }
}
