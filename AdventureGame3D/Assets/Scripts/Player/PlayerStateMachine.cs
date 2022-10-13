using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine{

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float MovementSpeed_FreeLook { get; private set; }
    [field: SerializeField] public float MovementSpeed_Target { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float DodgeCooldown { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] [field: NonReorderable] public Attack[] Attacks { get; private set; }
    [field: SerializeField] [field: NonReorderable] public Attack[] HeavyAttacks { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    private void Start() {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
    private void OnEnable() {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDeath;
    }
    private void OnDisable() {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDeath;
    }
    private void HandleTakeDamage() {
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDeath() {
        SwitchState(new PlayerDeathState(this));
    }
    public void SetDodgeTime(float dodgeTime) {
        PreviousDodgeTime = dodgeTime;
    }
}
