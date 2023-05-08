using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasillosCollider : MonoBehaviour
{
    public GameObject Letras;
    //public WriteText writeText;
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Letras.SetActive(true);
            //writeText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Letras.SetActive(false);
            //writeText.enabled = false;

        }
    }
}
