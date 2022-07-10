using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour {

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float drag;
    private float veritcalVelocity;
    [SerializeField] private Vector3 impact;
    private Vector3 dampVelocity;

    private void Update() {
        if (veritcalVelocity < 0 && characterController.isGrounded) {
            veritcalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else {
            veritcalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact,Vector3.zero, ref dampVelocity, drag);
    }

    public Vector3 ForceMovement() {
        return impact + Vector3.up * veritcalVelocity;
    }

    public void AddForce(Vector3 force) {
        impact += force;
    }
}
