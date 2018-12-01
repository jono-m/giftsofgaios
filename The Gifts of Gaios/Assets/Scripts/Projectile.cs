using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
    public float lifetime;
    public float moveSpeed;
    public float damage;

    public List<Team> unaffectedTeams;

    private Rigidbody2D rb;

    private float spawnTime;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = (Vector2)transform.right * moveSpeed;
        if(Time.time - spawnTime >= lifetime) {
            Debug.Log("Disintegrated.");
            Destroy(gameObject);
        }
	}

    public void Fire(Vector2 fromWhere, Vector2 direction) {
        transform.position = fromWhere;
        transform.right = direction.normalized;
        spawnTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        TeamAssigner teamAssigner = collider.GetComponent<TeamAssigner>();
        if (teamAssigner != null && unaffectedTeams.Contains(teamAssigner.team)) {
            return;
        } else {
            Debug.Log("Hit" + collider + "!");

            HealthTracker healthTracker = collider.GetComponent<HealthTracker>();
            if(healthTracker != null) {
                healthTracker.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
