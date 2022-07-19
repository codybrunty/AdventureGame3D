using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

    protected float GetNormalizedTime(Animator animator, string animationName) {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextState = animator.GetNextAnimatorStateInfo(0);

        if (currentState.IsName(animationName) && nextState.IsName(animationName)) {
            return 0f;
        }
        else if (currentState.IsName(animationName)) {
            return currentState.normalizedTime;
        }
        else {
            return 0f;
        }
    }

}