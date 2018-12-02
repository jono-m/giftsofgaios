using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GoG/Weapons/Projectile Weapon")]
public class ProjectileWeapon : Weapon {
    public float lifetime;
    public float launchSpeed;
    public float gravity;

    public bool autoTarget;

    public float autoTargetDeltaTime;

    public ProjectileController projectileToFirePrefab;

    public override void Use(CharacterAttackController attackController, Vector2 target) {
        Team firingTeam = attackController.GetComponent<TeamAssigner>().team;

        Vector2 firePoint = attackController.attackBasePoint.transform.position;
        Vector2 delta = target - firePoint;

        Vector2 fireDirection = delta.normalized;

        bool isRight = delta.x > 0;

        if (autoTarget) {
            float sqrtTerm = Mathf.Pow(launchSpeed, 4) - gravity * (gravity * Mathf.Pow(delta.x, 2) + 2 * delta.y * Mathf.Pow(launchSpeed, 2));
            if(sqrtTerm < 0) {
                // Cant reach target
                return;
            }

            float sol1 = Mathf.Atan((Mathf.Pow(launchSpeed, 2) + Mathf.Sqrt(sqrtTerm)) / (Mathf.Abs(delta.x * gravity)));
            float sol2 = Mathf.Atan((Mathf.Pow(launchSpeed, 2) - Mathf.Sqrt(sqrtTerm)) / (Mathf.Abs(delta.x * gravity)));

            Vector2 fireDirection1 = new Vector2(Mathf.Cos(sol1) * (isRight ? 1.0f : -1.0f), Mathf.Sin(sol1));
            Vector2 fireDirection2 = new Vector2(Mathf.Cos(sol2) * (isRight ? 1.0f : -1.0f), Mathf.Sin(sol2));

            bool canHit1 = true;
            bool canHit2 = true;

            // Check if we can hit the target without a collision:
            foreach(RaycastHit2D hit in PhysicsUtility.GravityRaycast(firePoint, fireDirection1 * launchSpeed, target, gravity, autoTargetDeltaTime)) {
                if(hit.collider.GetComponent<AutoProjectileBlocker>() != null) {
                    canHit1 = false;
                    break;
                }
            }
            foreach (RaycastHit2D hit in PhysicsUtility.GravityRaycast(firePoint, fireDirection2 * launchSpeed, target, gravity, autoTargetDeltaTime)) {
                if (hit.collider.GetComponent<AutoProjectileBlocker>() != null) {
                    canHit2 = false;
                    break;
                }
            }

            float? bestAngle = null;

            if (canHit1 && canHit2) {
                float time1 = (launchSpeed * Mathf.Sin(sol1) + Mathf.Sqrt(Mathf.Pow(launchSpeed * Mathf.Sin(sol1), 2) + Mathf.Abs(2 * gravity * delta.y))) / gravity;
                float time2 = (launchSpeed * Mathf.Sin(sol2) + Mathf.Sqrt(Mathf.Pow(launchSpeed * Mathf.Sin(sol2), 2) + Mathf.Abs(2 * gravity * delta.y))) / gravity;

                bestAngle = (time1 > time2) ? sol2 : sol1;
            } else {
                if(canHit1) {
                    bestAngle = sol1;
                } else if (canHit2) {
                    bestAngle = sol2;
                }
            }

            if(!bestAngle.HasValue) {
                return;
            }

            fireDirection = new Vector2(Mathf.Cos(bestAngle.Value) * (isRight ? 1.0f : -1.0f), Mathf.Sin(bestAngle.Value));
        }

        ProjectileController newProjectile = Instantiate(projectileToFirePrefab.gameObject).GetComponent<ProjectileController>();
        newProjectile.Fire(firePoint, fireDirection, firingTeam, this);
    }
}
