using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuricataScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rb;

    private bool leftDir = true;

    public Transform GroundCheck;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (leftDir)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        GroundDetection();
    }

    void GroundDetection()
    {
        if(!Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        leftDir = !leftDir;

        Vector3 SuriScale = transform.localScale;

        if (leftDir)
        {
            SuriScale.x = Mathf.Abs(SuriScale.x);
        }
        else
        {
            SuriScale.x = -Mathf.Abs(SuriScale.x);
        }

        transform.localScale = SuriScale;
    }

    //Daño al jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    //Daño por balas
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
