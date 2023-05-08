using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLite : MonoBehaviour
{
    //Players Array
    public GameObject[] players; 
    private int currentPlayerIndex = 0; 
    public GameObject RevolverBullet;

    //CheckIfOnGround
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    private Rigidbody2D rb;

    public Animator[] Anim;

    //Jump
    float jumpPower;
    float jump2Power;
    private bool isOnGround;
    private bool hasJumped;
    private bool hasdoubleJumped;
    private bool doubleJump;

    //Walk
    float speed = 5f;
    private float h;


    //Shoot
    public GameObject[] Balas;
    int BalasIndex = 0;

    public GameObject BalasJugador1;


    //Script
    public PlayerDamage PDS;

    //Dash
    private bool canDash = true;
    float dashVelocity = 3.5f;
    float dashTime = 0.1f;
    float dashWait = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i == currentPlayerIndex)
            {
                players[i].SetActive(true);
            }
            else
            {
                players[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        SwitchPlayers();
        ShootAnim();
        CheckIfOnGround();
        PlayerJump();
        Player1Stats();
        DeadAnimation();
    }

    void FixedUpdate()
    {
        PlayerWalk();

    }

    private void SwitchPlayers()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            players[currentPlayerIndex].SetActive(false);
            

            currentPlayerIndex++;
            if (currentPlayerIndex >= players.Length)
            {
                currentPlayerIndex = 0;
            }

            players[currentPlayerIndex].SetActive(true);
            Anim[currentPlayerIndex].SetTrigger("Puff");
        }
    }

    void PlayerWalk()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {

            //rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);

            ChangeDirection(1);
            Anim[0].SetBool("Run", false);
            if (currentPlayerIndex == 0 && Input.GetKey(KeyCode.LeftShift))
            {
                Anim[0].SetBool("Run", true);
                //rb.velocity = new Vector2(speed *1.5f, rb.velocity.y);
                transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);
            }
            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentPlayerIndex == 1)
            {
                dashTime = 1f;
                Debug.Log("Dash");
                StartCoroutine(Dash());
            }
        }
        else if (h < 0)
        {

            //rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);

            ChangeDirection(-1);
            Anim[0].SetBool("Run", false);
            if (currentPlayerIndex == 0 && Input.GetKey(KeyCode.LeftShift))
            {
                Anim[0].SetBool("Run", true);
                //rb.velocity = new Vector2(-speed * 1.5f, rb.velocity.y);
                transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);
            }
            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentPlayerIndex == 1)
            {
                dashTime = 1f;
                Debug.Log("Dash");
                StartCoroutine(Dash());
            }
        }
        else
        {
            //rb.velocity = new Vector2(0f, rb.velocity.y);
            transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);
            Anim[0].SetBool("Run", false);
        }

        AnimationIntParameters("Speed");
        
    }

    void ChangeDirection(int direction)
    {
        Vector3 Scale = transform.localScale;
        Scale.x = direction;
        transform.localScale = Scale;
    }

    public void AnimationIntParameters(string animationParameter)
    {
        for (int i = 0; i < Anim.Length; i++)
        {
            Anim[i].SetInteger(animationParameter, Mathf.Abs((int)h));
        }
    }

    void CheckIfOnGround()
    {
        isOnGround = Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.1f, GroundLayer);

        if (isOnGround)
        {
            canDash = true;
            if (hasJumped)
            {
                hasJumped = false;

                AnimationParameters("Jump", false);
            }
            else if (hasdoubleJumped)
            {
                hasdoubleJumped = false;
                AnimationParameters("DoubleJump", false);

            }
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

                if (Input.GetKeyDown(KeyCode.Space) && currentPlayerIndex == 1)
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

    public void AnimationParameters(string animationParameter, bool animationStatus)
    {
        for (int i = 0; i < Anim.Length; i++)
        {
            Anim[i].SetBool(animationParameter, animationStatus);
        }
    }

    private void ShootAnim()
    {
        if(currentPlayerIndex != 0)
        {
            BalasJugador1.SetActive(false);
        }
        else
        {
            BalasJugador1.SetActive(true);
        }

        if(currentPlayerIndex == 0 && Input.GetKeyDown(KeyCode.W))
        {
            if (BalasIndex < 7)
            {
                GameObject bullet = Instantiate(RevolverBullet, transform.position, Quaternion.identity);
                bullet.GetComponent<RevolverBullet>().Speed *= transform.localScale.x;
                Anim[0].SetTrigger("Shoot");
                Balas[BalasIndex].SetActive(false);
                BalasIndex++;
            }
            else if (BalasIndex >= 7)
            {
                BalasIndex = 7;
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            Debug.Log("Shoot");
            gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
        if (collision.CompareTag("DropBala"))
        {
            BalasIndex--;

            if (BalasIndex <= 0)
            {
                BalasIndex = 0;
            }

            Balas[BalasIndex].SetActive(true);
            
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;

        if (h < 0)
        {
            Anim[1].SetTrigger("Dash");
            
            if (dashTime > 0)
            {
                rb.velocity = new Vector2(-speed * dashVelocity, rb.velocity.y);
                dashTime -= Time.deltaTime;
            }
            if (dashTime < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                dashTime = 2;
            }
        }
        else
        {
            Anim[1].SetTrigger("Dash");
            if (dashTime > 0)
            {
                rb.velocity = new Vector2(speed * dashVelocity, rb.velocity.y);
                dashTime -= Time.deltaTime;
            }
            if (dashTime < 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                dashTime = 2;
            }
            //rb.velocity = new Vector2(speed * dashVelocity, rb.velocity.y);
        }

        yield return new WaitForSeconds(dashWait);
    }
    public void Player1Stats()
    {
        speed = players[currentPlayerIndex].GetComponent<Player>().speed;
        jumpPower = players[currentPlayerIndex].GetComponent<Player>().jumpPower;
        jump2Power = players[currentPlayerIndex].GetComponent<Player>().jump2Power;
        dashVelocity = players[currentPlayerIndex].GetComponent<Player>().dashVelocity;
        dashTime = players[currentPlayerIndex].GetComponent<Player>().dashTime;
        dashWait = players[currentPlayerIndex].GetComponent<Player>().dashWait;
    }

    void DeadAnimation()
    {
        if (PDS.PlayerDead)
        {
            
            Anim[currentPlayerIndex].SetBool("Dead", true);
            this.enabled = false;
        }
    }
}
