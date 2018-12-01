using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour {
    public float maxHealth = 100.0f;

    public float currentHealth { get; private set; }

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
