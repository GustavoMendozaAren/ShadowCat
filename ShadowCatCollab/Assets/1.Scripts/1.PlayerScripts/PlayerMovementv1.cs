using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementv1 : MonoBehaviour
{
    
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator Anim;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    //Jump Variables
    public float jumpPower = 5f;
    public float jump2Power = 3f;
    private bool isOnGround;
    private bool hasJumped;
    private bool hasdoubleJumped;
    private bool doubleJump;
    

    //Dash Variables
    public float dashVelocity = 3.5f;
    public float dashTime = 0.1f;
    private float initialgravity;
    private bool canDash = true;
    private bool canMove = true;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        initialgravity = rb.gravityScale;
    }

    private void Update()
    {
        CheckIfOnGround();
        PlayerJump();
        
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            PlayerWalk();
        }
        
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
            else if(hasdoubleJumped)
            {
                hasdoubleJumped = false;
                Anim.SetBool("DoubleJump", false);

            }

            canDash = true;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                hasJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                Anim.SetBool("Jump", true);
                doubleJump = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (doubleJump)
                    {
                        hasdoubleJumped = true;
                        Anim.SetBool("DoubleJump", true);
                        rb.velocity = new Vector2(rb.velocity.x, jump2Power);
                        doubleJump = false;
                    }
                }
            }
        }
        

    }

    private IEnumerator Dash()
    {
        canMove = false;
        canDash = false;
        rb.gravityScale = 0;

        rb.velocity = new Vector2(dashVelocity * transform.localScale.x, 0);
        
        yield return new WaitForSeconds(dashTime);

        canMove = true;
        rb.gravityScale = initialgravity;
    }
}
