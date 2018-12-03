using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnAroundCollider : MonoBehaviour {
    public bool turnAroundOnCollision;
    
    public UnityEvent ShouldTurnAround;

    private Collider2D myCollider;

    private void Start() {
        myCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        if(!turnAroundOnCollision) {
            Collider2D[] colliders = new Collider2D[10];
            int nColl = myCollider.OverlapCollider(new ContactFilter2D(), colliders);

            bool seesGround = false;
            for (int i = 0; i < nColl; i++) {
                if(colliders[i].gameObject.isStatic) {
                    seesGround = true;
                }       
            }
            if(!seesGround) {
                ShouldTurnAround.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.isTrigger && turnAroundOnCollision && collision.GetComponent<GaiosChoiceApplier>() == null && collision.gameObject != transform.parent.gameObject) {
            ShouldTurnAround.Invoke();
        }
    }
}
