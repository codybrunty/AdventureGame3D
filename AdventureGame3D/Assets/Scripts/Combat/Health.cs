using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    public event Action OnTakeDamage;
    public event Action OnDie;
    private bool isInvunerable=false;

    public bool IsDead => health == 0;

    private void Start() {
        health = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (health == 0) { return;}
        if (isInvunerable) { return; }

        health = Mathf.Clamp(health - damage,0,int.MaxValue);
        Debug.Log(gameObject.name + ": " + health + " HP");

        if (health == 0) {
            OnDie?.Invoke();
            return;
        }

        OnTakeDamage?.Invoke();
    }
    public void SetInvunerable(bool isInvunerable) {
        this.isInvunerable = isInvunerable;
    }
}
