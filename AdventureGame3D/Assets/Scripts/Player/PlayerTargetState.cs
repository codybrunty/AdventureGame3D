using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    public PlayerTargetState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int TargetBlendTreeHash = Animator.StringToHash("TargetBlendTree");
    private readonly int TargetForwardSpeedHash = Animator.StringToHash("TargetForwardSpeed");
    private readonly int TargetRightSpeedHash = Animator.StringToHash("TargetRightSpeed");
    private const float AnimatorDampTime = 0.1f;

    public override void Enter() {
        stateMachine.InputReader.TargetEvent += CancelTarget;
        stateMachine.InputReader.AttackEvent += Attack;
        stateMachine.Animator.Play(TargetBlendTreeHash);
    }

    public override void Tick(float deltaTime) {
        if (stateMachine.Targeter.CurrentTarget == null) {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.MovementSpeed_Target, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }
    public override void Exit() {
        stateMachine.InputReader.TargetEvent -= CancelTarget;
        stateMachine.InputReader.AttackEvent -= Attack;
    }

    public void CancelTarget() {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement() {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        return movement;
    }

    private void UpdateAnimator(float deltaTime) {
        if (stateMachine.InputReader.MovementValue.x == 0f && stateMachine.InputReader.MovementValue.y == 0f) {
            stateMachine.Animator.SetFloat(TargetForwardSpeedHash, 0f, AnimatorDampTime, deltaTime); 
            stateMachine.Animator.SetFloat(TargetRightSpeedHash, 0f, AnimatorDampTime, deltaTime);
        }
        else{
            stateMachine.Animator.SetFloat(TargetForwardSpeedHash, stateMachine.InputReader.MovementValue.y, AnimatorDampTime, deltaTime);
            stateMachine.Animator.SetFloat(TargetRightSpeedHash, stateMachine.InputReader.MovementValue.x, AnimatorDampTime, deltaTime);
        }
    }

    private void Attack() {
        stateMachine.SwitchState(new PlayerAttackState(stateMachine,0));
    }

}
