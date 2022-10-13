using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour {

    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent NavMeshAgent;
    [SerializeField] private float drag;
    private float veritcalVelocity;
    private Vector3 impact;
    private Vector3 dampVelocity;

    private void Update() {
        if (veritcalVelocity < 0 && characterController.isGrounded) {
            veritcalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else {
            veritcalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact,Vector3.zero, ref dampVelocity, drag);
        if (NavMeshAgent != null) {
            if (impact.sqrMagnitude < 0.2f*0.2f ) {
                impact = Vector3.zero;
                NavMeshAgent.enabled = true;
            }
        }
    }

    public Vector3 ForceMovement() {
        return impact + Vector3.up * veritcalVelocity;
    }

    public void AddForce(Vector3 force) {
        impact += force;
        if (NavMeshAgent!=null) {
            NavMeshAgent.enabled = false;
        }
    }

    public void Jump(float jumpForce) {
        veritcalVelocity += jumpForce;
    }

}
