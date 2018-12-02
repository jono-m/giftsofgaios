using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : MonoBehaviour {
    public float movementSpeed;
    public float airMovementSpeed;
    public float jumpForce;
    public float gravity;

    public float recoverySpeed;
    public Vector2 knockbackForce;

    public float groundedDistance;

    public bool facingLeft { get; private set; }

    private bool isGrounded = false;

    private bool usedDoubleJump = false;

    private bool isKnockedBack = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float hVelocity = 0.0f;

        if (isKnockedBack) {
            if (rb.velocity.magnitude <= recoverySpeed) {
                isKnockedBack = false;
            } else {
                hVelocity = rb.velocity.x;
            }
        } else {
            if (Input.GetKey(KeyCode.A)) {
                hVelocity -= movementSpeed;
                facingLeft = true;
            }

            if (Input.GetKey(KeyCode.D)) {
                hVelocity += movementSpeed;
                facingLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                DoJump();
            }

            if (!isGrounded) {
                hVelocity *= airMovementSpeed;
            }
        }

        rb.velocity = new Vector2(hVelocity, rb.velocity.y - gravity * Time.deltaTime);

        isGrounded = false;
        foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.position, Vector2.down, groundedDistance)) {
            if (hit.collider.GetComponent<JumpResetter>() != null) {
                isGrounded = true;
                usedDoubleJump = false;
            }
        }
    }

    private void DoJump() {
        if(isGrounded || !usedDoubleJump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (!isGrounded) {
                usedDoubleJump = true;
            } else {
                isGrounded = false;
            }
        }
    }

    public void DoKnockback() {
        isKnockedBack = true;

        Vector2 force = new Vector2(knockbackForce.x * (facingLeft ? 1.0f : -1.0f), knockbackForce.y);

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundedDistance);
    }
}
