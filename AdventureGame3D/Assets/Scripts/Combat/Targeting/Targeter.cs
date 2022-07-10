using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour{

    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    private Camera mainCam;

    private void Start() {
        mainCam = Camera.main;    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Target>(out Target target)) {
            AddTarget(target);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<Target>(out Target target)) {
            RemoveTarget(target);
        }
    }

    public bool SelectTarget() {
        if (targets.Count == 0) { return false; }

        List<Target> targetsOnScreen = new List<Target>();
        List<float> targetValues = new List<float>();
        for (int i = 0; i < targets.Count; i++) {
            Vector2 viewPos = mainCam.WorldToViewportPoint(targets[i].transform.position);
            if(viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1) {
                continue;
            }
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            float value = toCenter.sqrMagnitude;
            //float value = (Mathf.Abs(viewPos.x - 0.5f)) + (Mathf.Abs(viewPos.y - 0.5f));
            targetsOnScreen.Add(targets[i]);
            targetValues.Add(value);
        }
        if (targetsOnScreen.Count == 0) { return false; }

        float smallestValue = float.MaxValue;
        int smallestValueIndex = 0;
        for (int i = 0; i < targetsOnScreen.Count; i++) {
            if(targetValues[i] < smallestValue) {
                smallestValue = targetValues[i];
                smallestValueIndex = i;
            }
        }
        CurrentTarget = targetsOnScreen[smallestValueIndex];

        cinemachineTargetGroup.AddMember(CurrentTarget.transform,1f,2f);
        return true;
    }
    public void CancelTarget() {
        if (CurrentTarget == null) { return; }
        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void AddTarget(Target target) {
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void RemoveTarget(Target target) {
        if (CurrentTarget == target) {
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

}
