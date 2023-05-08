using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchPlayerv1 : MonoBehaviour
{
    private bool player1status;
    private bool player2status;

    private int nplayer;
    private int nGetValues;
    
    //Players
    public GameObject player1;
    public GameObject player2;

    //Script de disparo del jugador
    private PlayerShootScript playershootScript;

    private Rigidbody2D rb;
    public Animator[] Anim;
    public SpriteRenderer[] spriteRenderes;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    //Variables
    float speed = 5f;
    float jumpPower;
    float jump2Power;
    float dashVelocity = 3.5f;
    float dashTime = 0.1f;
    
    
    private bool isOnGround;
    private bool hasJumped;
    private bool hasdoubleJumped;
    private bool doubleJump;
    

    //Dash Variables
   
    private float initialgravity;
    private bool canDash = true;
    private bool canMove = true;
    private bool isDead;

    private float h;
    private void Awake()
    {
        playershootScript = GetComponent<PlayerShootScript>();
        rb = GetComponent<Rigidbody2D>();
        initialgravity = rb.gravityScale;

        nplayer = 1;
        nGetValues = 1;
        isDead = false;
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

        if (isDead)
        {
            SceneManager.LoadScene("SampleScene Jacob");
        }

        switch (nplayer)
        {
            case 1: Player1Active();
                break;
            case 2: Player2Active();
                break;
        }

        switch (nGetValues)
        {
            case 1: GetPlayer1();
                break;
            case 2: GetPlayer2();
                break;
        }

        ChangePlayers();
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
        h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            ChangeDirection(1);
        } else if (h < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        AnimationIntParameters("Speed");
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

                AnimationParameters("Jump", false);
            }
            else if(hasdoubleJumped)
            {
                hasdoubleJumped = false;
                AnimationParameters("DoubleJump", false);

            }

            canDash = true;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isOnGround)
            {
                hasJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                AnimationParameters("Jump", true);
                doubleJump = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (doubleJump)
                    {
                        hasdoubleJumped = true;
                        AnimationParameters("DoubleJump", true);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            isDead = true;
        }
    }

    public void SpriteRendererStatus(bool srStatus)
    {
        for (int i = 0; i < spriteRenderes.Length; i++)
        {
            spriteRenderes[i].flipX = srStatus;
        }
    }

    public void AnimationParameters(string animationParameter, bool animationStatus)
    {
        for (int i = 0; i < Anim.Length; i++)
        {
            Anim[i].SetBool(animationParameter, animationStatus);
        }
    }
    public void AnimationIntParameters(string animationParameter)
    {
        for (int i = 0; i < Anim.Length; i++)
        {
            Anim[i].SetInteger(animationParameter,Mathf.Abs((int)h));
        }
    }

    public void ChangePlayers()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nplayer++;
            if (nplayer > 2) nplayer = 1;
            nGetValues = nplayer;
        }
    }

    public void Player1Active()
    {
        player1.SetActive(true);
        player1status = true;
        player2.SetActive(false);
        player2status = false;
        //Activacion del script de disparo para el personaje 1
        playershootScript.enabled = true;
    }

    public void Player2Active()
    {
        player1.SetActive(false);
        player1status = false;
        player2.SetActive(true);
        player2status = true;
        //Desactivacion del script de disparo para el personaje 2
        playershootScript.enabled = false;
    }

    public void GetPlayer1()
    {
        speed = player1.GetComponent<Player>().speed;
        jumpPower = player1.GetComponent<Player>().jumpPower;
        jump2Power = player1.GetComponent<Player>().jump2Power;
        dashVelocity = player1.GetComponent<Player>().dashVelocity;
        dashTime = player1.GetComponent<Player>().dashTime;
    }
    public void GetPlayer2()
    {
        speed = player2.GetComponent<Player>().speed;
        jumpPower = player2.GetComponent<Player>().jumpPower;
        jump2Power = player2.GetComponent<Player>().jump2Power;
        dashVelocity = player2.GetComponent<Player>().dashVelocity;
        dashTime = player2.GetComponent<Player>().dashTime;
    }
    
}
