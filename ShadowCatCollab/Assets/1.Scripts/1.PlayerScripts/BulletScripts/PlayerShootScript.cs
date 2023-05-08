using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{

    public GameObject RevolverBullet;

    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject bullet = Instantiate(RevolverBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<RevolverBullet>().Speed *= transform.localScale.x;
        }
    }
}
