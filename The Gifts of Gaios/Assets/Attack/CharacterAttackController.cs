using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamAssigner))]
public class CharacterAttackController : MonoBehaviour {
    public Weapon weapon;
    public AttackBehavior behavior;

    public Transform attackBasePoint;

    private float lastWeaponUseTime = Mathf.NegativeInfinity;

    public bool IsWeaponOnCooldown() {
        if (weapon == null) {
            return true;
        } else {
            if (Time.time - lastWeaponUseTime >= weapon.cooldown) {
                return false;
            } else {
                return true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if(behavior != null) {
            behavior.DoBehavior(this);
        }
	}

    public void Attack(Vector2 target) {
        if(weapon != null && !IsWeaponOnCooldown()) {
            weapon.Use(this, target);
            lastWeaponUseTime = Time.time;
        }
    }

    private void OnDrawGizmos() {
        if(behavior != null) {
            behavior.DrawAttackGizmos(this);
        }
    }
}
