using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour {
    private Team firingTeam;
    private Rigidbody2D rb;
    private float spawnTime;

    private ProjectileWeapon weaponInfo;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity += Vector2.down * weaponInfo.gravity * Time.deltaTime;
        transform.right = rb.velocity.normalized;
        if(Time.time - spawnTime >= weaponInfo.lifetime) {
            Debug.Log("Disintegrated.");
            Destroy(gameObject);
        }
	}

    public void Fire(Vector2 fromWhere, Vector2 direction, Team firingTeam, ProjectileWeapon weaponInfo) {
        transform.position = fromWhere;
        transform.right = direction.normalized;
        spawnTime = Time.time;

        this.firingTeam = firingTeam;
        this.weaponInfo = weaponInfo;
        rb.velocity = direction.normalized * weaponInfo.launchSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        TeamAssigner teamAssigner = collider.GetComponent<TeamAssigner>();
        if (collider.isTrigger || (teamAssigner != null && firingTeam == teamAssigner.team)) {
            return;
        } else {
            Debug.Log("Hit " + collider);
            HealthTracker healthTracker = collider.GetComponent<HealthTracker>();
            if (healthTracker != null) {
                healthTracker.TakeDamage(weaponInfo.damage);
            }
            Destroy(gameObject);
        }
    }
}
