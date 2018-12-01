using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour {
    public float fireCooldown;

    public float fireSpawnRadius;

    public Projectile projectileToFirePrefab;

    private float lastFireTime = Mathf.NegativeInfinity;
    
    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButton(0)) {
            if (Time.time - lastFireTime >= fireCooldown) {
                FireAtMouse();
            }
        }
	}

    private void FireAtMouse() {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = target - (Vector2)transform.position;
        Vector2 firePoint = (Vector2)transform.position + direction.normalized * fireSpawnRadius;

        Projectile newProjectile = Instantiate(projectileToFirePrefab.gameObject).GetComponent<Projectile>();
        newProjectile.Fire(firePoint, direction);
        lastFireTime = Time.time;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, fireSpawnRadius);
    }
}
