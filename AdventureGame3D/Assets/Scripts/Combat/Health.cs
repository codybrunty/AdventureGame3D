using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;

    private void Start() {
        health = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (health == 0) { return; } 
        health = Mathf.Clamp(health - damage,0,int.MaxValue);
    }

}
