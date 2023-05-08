using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BandidoEnemy : MonoBehaviour
{

    public float moveSpeed = 1f;
    private Rigidbody2D MyBody;
    private Animator Anim;

    private bool moveRight;

    public Transform GroundCollision;

    //Light
    public LightEnemy LES;

    private void Awake()
    {
        MyBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        CheckCollision();
    }

    void MoveEnemy()
    {
        if (moveRight)
        {
            MyBody.velocity = new Vector2(moveSpeed, MyBody.velocity.y);
        }
        else
        {
            MyBody.velocity = new Vector2(-moveSpeed, MyBody.velocity.y);
        }
    }

    void CheckCollision()
    {
        if (!Physics2D.Raycast(GroundCollision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveRight = !moveRight;

        Vector3 tempScale = transform.localScale;

        if (moveRight)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    //Daño por balas
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Bullet"))
        {
            LES.DimLight = true;
            Anim.SetBool("Death", true);
            moveSpeed = 0f;
            GetComponent<BandidoEnemy>().enabled = false;
            Invoke(nameof(DeactivateEnemy), 1.5f);
        }
    }

    void DeactivateEnemy()
    {
        gameObject.SetActive(false);
    }


}
