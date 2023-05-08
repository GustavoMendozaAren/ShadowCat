using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody2D rb;
    private Animator Anim;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public float jumpPower = 5f;
    private bool isOnGround;
    private bool hasJumped;
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckIfOnGround();
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk ()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            ChangeDirection(5);
        } else if (h < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            ChangeDirection(-5);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        Anim.SetInteger("Speed", Mathf.Abs((int)h));
    }

    void ChangeDirection(int direction)
    {
        Vector3 Scale = transform.localScale;
        Scale.x = direction;
        transform.localScale = Scale;
    }

    void CheckIfOnGround()
    {
        isOnGround = Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.1f, GroundLayer);

        if (isOnGround)
        {
            if (hasJumped)
            {
                hasJumped = false;

                Anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isOnGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                hasJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);

                Anim.SetBool("Jump", true);
            }
        }
    }
}
