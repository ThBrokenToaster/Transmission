using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float airSpeed;
    Rigidbody2D playerRB;
    bool right;
    bool onGround;
    bool onWall;
    bool onWallBack;
    float groundCollisionRadius = .2f;
    public LayerMask ground;
    public Transform groundCheck;
    public Transform wallCheck;
    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float verticalSpeed;
    public float wallJump;
    public float stickyForce;




	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody2D>();
        right = true;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Jumping Stuff
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCollisionRadius, ground);
        onWall = Physics2D.OverlapCircle(wallCheck.position, groundCollisionRadius, ground);



        //Moving
        float move = Input.GetAxis("Horizontal");
//        if (onGround)
//        {
            playerRB.velocity = new Vector2(move * speed, playerRB.velocity.y);
 //       } else
//        {
 //           playerRB.AddForce(new Vector2(move * airSpeed, 0));
 //       }
		

        if (move < 0 && right)
        {
            flip();
        } 
        else if (move > 0 && !right)
        {
            flip();
        }

	}

    void flip()
    {
        right = !right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Update()
    {
        if (onGround && Input.GetButtonDown("Jump"))
        {
            onGround = false;
            playerRB.AddForce(new Vector2(0, jumpHeight));
        }

        if (onWallBack && right)
        {
            playerRB.AddForce(new Vector2(-1 * wallJump, 0));
        }




        if (onWall && Input.GetButtonDown("Jump"))
        {
            onWall = false;
            playerRB.AddForce(new Vector2(0, jumpHeight));
            if (right)
            {
                playerRB.AddForce(new Vector2(-1 * wallJump, 0));

            } else
            {
                playerRB.AddForce(new Vector2(1 * wallJump, 0));
            }
        }
;

        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }
}
