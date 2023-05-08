using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBala : MonoBehaviour
{
    float contador = 0, y = 0;
    bool suma;
    private void Update()
    {

        if(y <= 0)
        {
            suma = true;
            contador = 0;
        }
        else if (y >= .001f)
        {
            suma = false;
            contador = 0;
        }

        if (suma)
        {
            contador += Time.deltaTime * .001f;

            y = 0 + contador;
            transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        } 
        else if (!suma)
        {
            contador += Time.deltaTime * .001f;

            y = .001f - contador;
            transform.position = new Vector3(transform.position.x, transform.position.y - y, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
