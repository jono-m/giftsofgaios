using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementController : MonoBehaviour {
    public float movementSpeed;
    public float airMovementSpeed;
    public float jumpForce;
    public float gravity;

    public bool facingLeft { get; private set; }

    private bool isGrounded = false;

    private bool usedDoubleJump = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float hVelocity = 0.0f;
	    if(Input.GetKey(KeyCode.A)) {
            hVelocity -= movementSpeed;
            facingLeft = true;
        }

        if (Input.GetKey(KeyCode.D)) {
            hVelocity += movementSpeed;
            facingLeft = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            DoJump();
        }

        if(!isGrounded) {
            hVelocity *= airMovementSpeed;
        }

        rb.velocity = new Vector2(hVelocity, rb.velocity.y - gravity * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Floor")) {
            isGrounded = true;
            usedDoubleJump = false;
        }
    }
}
