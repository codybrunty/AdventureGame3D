using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour{

    [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private int weaponDamage=10;

    private void OnTriggerEnter(Collider other) {
        if (other == myCollider) { return; }
        if (alreadyCollidedWith.Contains(other)) { return; }
        else {
            alreadyCollidedWith.Add(other);
        }

        if (other.TryGetComponent<Health>(out Health health)) {
            health.TakeDamage(weaponDamage);
        }
    }

    private void OnEnable() {
        alreadyCollidedWith.Clear();
    }

    public void SetDamage(int damage) {
        weaponDamage = damage;
    }

}
